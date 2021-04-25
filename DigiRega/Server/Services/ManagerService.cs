using AutoMapper;
using DigiRega.Server.Data;
using DigiRega.Server.Model;
using DigiRega.Server.Services.Exceptions;
using DigiRega.Server.Utilities;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public class ManagerService : ServiceBase, IManagerService
    {
        private readonly IEmailingQueueService emailingQueue;

        public ManagerService(DigiRegaDbContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IEmailingQueueService emailingQueue, ILogger<ManagerService> logger)
            : base(db, mapper, httpContextAccessor, logger)
        {
            this.emailingQueue = emailingQueue;
        }

        public async Task<ServiceResponse<int>> AddManagerAsync(AddManagerDto managerDto)
        {
            logger.LogInformation("Adding new manager {FirstName} {LastName} for club {ClubId}.",
                managerDto.FirstName, managerDto.LastName, managerDto.ClubId);

            var manager = mapper.Map<Manager>(managerDto);

            // Generate a new secret and also store it as password.
            manager.Secret = await GenerateUniqueSecret();
            (manager.PasswordHash, manager.PasswordSalt) = HashingHelper.CreateHash(manager.Secret);

            db.Managers.Add(manager);
            await db.SaveChangesAsync();

            // The DTO contains a flag if an invitation email should be sent out.
            if (managerDto.SendEmail)
            {
                // We want to know the club details for emailing, they must be loaded explicitly.
                await db.Entry(manager).Reference(m => m.Club).LoadAsync();
                await emailingQueue.QueueInvitationAsync(manager);
            }

            logger.LogDebug("Added new manager {FirstName} {LastName} for club {ClubId} with ID {Id}.",
                manager.FirstName, manager.LastName, manager.ClubId, manager.Id);

            return manager.Id;
        }

        public async Task<ServiceResponse<IList<GetManagerDto>>> GetAllManagersAsync(int? clubId = null)
        {
            logger.LogInformation("Getting all managers.");

            IQueryable<Manager> query = db.Managers;

            // Apply club filter if set.
            if (clubId != null)
            {
                logger.LogInformation("Limiting managers to club {ClubId} only.", clubId);
                query = query.Where(m => m.ClubId == clubId);
            }

            var managers = await query.ToListAsync();
            return mapper.Map<List<GetManagerDto>>(managers);
        }

        public async Task<ServiceResponse<GetManagerDto>> GetManagerAsync(int id)
        {
            logger.LogInformation("Getting manager {Id}.", id);

            var manager = await db.Managers.SingleOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                logger.LogWarning("Manager {Id} not found.", id);
                throw new NotFoundServiceException($"No manager found for ID {id}.");
            }

            return mapper.Map<GetManagerDto>(manager);
        }

        public async Task<ServiceResponse> RemoveManagerAsync(int id)
        {
            logger.LogInformation("Removing manager {Id}.", id);

            var manager = await db.Managers.SingleOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                logger.LogWarning("Manager {Id} not found.", id);
                throw new NotFoundServiceException($"No manager found for ID {id}.");
            }

            db.Managers.Remove(manager);
            await db.SaveChangesAsync();

            return SuccessfulServiceResponse();
        }

        public async Task<ServiceResponse> UpdateManagerAsync(int id, UpdateManagerDto update)
        {
            logger.LogInformation("Updating manager {Id}", id);

            var manager = await db.Managers.SingleOrDefaultAsync(m => m.Id == id);
            if (manager == null)
            {
                logger.LogWarning("Manager {Id} not found.", id);
                throw new NotFoundServiceException($"No manager found for ID {id}.");
            }

            manager.FirstName = update.FirstName;
            manager.LastName = update.LastName;
            manager.EmailAddress = update.EmailAddress;

            // Send email if requested in update.
            if (update.SendEmail)
            {
                // Generate new secret to invalidate old credentials.
                manager.Secret = await GenerateUniqueSecret();
                (manager.PasswordHash, manager.PasswordSalt) = HashingHelper.CreateHash(manager.Secret);

                // We want to know the club details for emailing, they must be loaded explicitly.
                await db.Entry(manager).Reference(m => m.Club).LoadAsync();
                await emailingQueue.QueueInvitationAsync(manager);
            }

            db.Managers.Update(manager);
            await db.SaveChangesAsync();

            return SuccessfulServiceResponse();
        }

        /// <summary>
        /// Generates a unique secret that is not used by other managers.
        /// </summary>
        /// <param name="retries">Number of retries to find a unique secret.</param>
        /// <returns>Unique random string to be used as secret.</returns>
        private async Task<string> GenerateUniqueSecret(int retries = 5)
        {
            for (int i = 0; i < retries; i++)
            {
                var secret = SecretHelper.GenerateSecret();

                if (!await db.Managers.AnyAsync(m => m.Secret == secret))
                {
                    return secret;
                }
            }

            // No ServiceException as this is a server-side failure.
            throw new Exception("Cannot find a unique secret, please try again later.");
        }
    }
}
