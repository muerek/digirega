using AutoMapper;
using DigiRega.Server.Data;
using DigiRega.Server.Model;
using DigiRega.Server.Services.Exceptions;
using DigiRega.Shared.Definitions;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Entry;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public class EntryService : ServiceBase, IEntryService
    {
        private readonly IEmailingQueueService emailingQueue;

        public EntryService(DigiRegaDbContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            IEmailingQueueService emailingQueue, ILogger<EntryService> logger)
            : base(db, mapper, httpContextAccessor, logger)
        {
            this.emailingQueue = emailingQueue;
        }

        public async Task<ServiceResponse<int>> AddCrewChangeAsync(AddCrewChangeDto crewChangeDto)
        {
            logger.LogInformation("Adding new crew change for club {ClubId} in race {RaceId}.", crewChangeDto.ClubId, crewChangeDto.RaceId);
            
            var crewChange = await ReadEntryAsync<CrewChange>(crewChangeDto);
            db.CrewChanges.Add(crewChange);
            await db.SaveChangesAsync();

            if (crewChangeDto.SendEmailConfirmation)
            {
                await QueueEmailConfirmationAsync(crewChange);
            }

            logger.LogDebug("Added new crew change for club {ClubId} in race {RaceId} with ID {Id}",
                crewChange.ClubId, crewChange.RaceId, crewChange.Id);

            return crewChange.Id;
        }

        public async Task<ServiceResponse<int>> AddLateEntryAsync(AddLateEntryDto lateEntryDto)
        {
            logger.LogInformation("Adding new late entry for club {ClubId} in race {RaceId}.", lateEntryDto.ClubId, lateEntryDto.RaceId);

            var lateEntry = await ReadEntryAsync<LateEntry>(lateEntryDto);
            db.LateEntries.Add(lateEntry);
            await db.SaveChangesAsync();

            if (lateEntryDto.SendEmailConfirmation)
            {
                await QueueEmailConfirmationAsync(lateEntry);
            }

            logger.LogDebug("Added new late entry for club {ClubId} in race {RaceId} with ID {Id}",
                lateEntry.ClubId, lateEntry.RaceId, lateEntry.Id);

            return lateEntry.Id;
        }

        public async Task<ServiceResponse<int>> AddWithdrawalAsync(AddWithdrawalDto withdrawalDto)
        {
            logger.LogInformation("Adding new withdrawal for club {ClubId} in race {RaceId}.", withdrawalDto.ClubId, withdrawalDto.RaceId);

            var withdrawal = await ReadEntryAsync<Withdrawal>(withdrawalDto);
            db.Withdrawals.Add(withdrawal);
            await db.SaveChangesAsync();

            if (withdrawalDto.SendEmailConfirmation)
            {
                await QueueEmailConfirmationAsync(withdrawal);
            }

            logger.LogDebug("Added new withdrawal for club {ClubId} in race {RaceId} with ID {Id}",
                withdrawal.ClubId, withdrawal.RaceId, withdrawal.Id);

            return withdrawal.Id;
        }

        public async Task<ServiceResponse<IList<GetEntryBriefDto>>> GetAllEntriesAsync(EntryStatus? status)
        {
            logger.LogInformation("Getting all entries.");
            
            IQueryable<Entry> entriesQuery = db.Entries;

            // Managers may only see entries of their own club.
            var user = await GetUserAsync();
            if (user is Manager manager)
            {
                logger.LogDebug("Selecting only entries for club {ClubId} to manager.", manager.ClubId);
                entriesQuery = entriesQuery.Where(e => e.ClubId == manager.ClubId);
            }

            // Apply status filter if given.
            if (status != null)
            {
                logger.LogDebug("Selecting only entries with status {EntryStatus}.", status);
                entriesQuery = entriesQuery.Where(e => e.Status == status);
            }

            var entries = await entriesQuery
                .OrderByDescending(e => e.SentAt)
                .Include(e => e.Club)
                .Include(e => e.Race)
                .ToListAsync();
            
            return mapper.Map<List<GetEntryBriefDto>>(entries);
        }

        public async Task<ServiceResponse<GetEntryDto>> GetEntryAsync(int id)
        {
            logger.LogInformation("Getting entry {Id}", id);
            
            var entry = await db.Entries.SingleOrDefaultAsync(e => e.Id == id);
            if (entry == null) { throw new NotFoundServiceException($"No entry found for ID {id}."); }

            if (!await ValidateAccessAsync(entry))
            {
                logger.LogWarning("User {UserId} tried to read entry {EntryId} but is not authorized.", GetUserId(), entry.Id);
                throw new ForbiddenServiceException();
            }

            // Load related data. Not using Include above to avoid large joins.
            await db.Entry(entry).Reference(e => e.SentBy).LoadAsync();
            await db.Entry(entry).Reference(e => e.Club).LoadAsync();
            await db.Entry(entry).Reference(e => e.Race).LoadAsync();

            return mapper.Map<GetEntryDto>(entry);
        }

        public async Task<ServiceResponse> UpdateStatusAsync(int id, UpdateEntryStatusDto statusUpdate)
        {
            logger.LogInformation("Updating entry {Id}", id);

            var entry = await db.Entries.SingleOrDefaultAsync(e => e.Id == id);
            if (entry == null) { throw new NotFoundServiceException($"No entry found for ID {id}"); }

            if (!await ValidateAccessAsync(entry))
            {
                logger.LogWarning("User {UserId} tried to update entry {EntryId} but is not authorized.", GetUserId(), entry.Id);
                throw new ForbiddenServiceException();
            }

            entry.Status = statusUpdate.Status;

            db.Entries.Update(entry);
            await db.SaveChangesAsync();

            return SuccessfulServiceResponse();
        }

        /// <summary>
        /// Converts a DTO for a new entry into a model object and makes sure all metadata is set.
        /// This includes <see cref="Entry.SentById"/>, <see cref="Entry.SentAt"/> and <see cref="Entry.Status"/>.
        /// </summary>
        /// <typeparam name="TEntry">Target model type, is some kind of <see cref="Entry"/>.</typeparam>
        /// <param name="dto">DTO to convert.</param>
        /// <returns><see cref="Entry"/> object mapped from DTO with all metadata set.</returns>
        private async Task<TEntry> ReadEntryAsync<TEntry>(AddEntryDto dto)
            where TEntry : Entry
        {
            // We need to know the club and responsible manager before mapping the DTO to the model class.
            // Those are optional in the DTO, but required in the model.
            // Check who is adding the entry.
            var user = await GetUserAsync();
            switch (user)
            {
                // For managers, it is always themselves, regardless what is set in the DTO.
                case Manager manager:
                    dto.ClubId = manager.ClubId;
                    dto.SentById = manager.Id;
                    break;
                // For entries added by staff or admins, the data has to be set in the DTO.
                case StaffMember when dto.SentById != null && dto.ClubId != null:
                    break;
                // If neither applies, we cannot process this entry.
                default:
                    logger.LogError("Cannot process new entry as responsible manager cannot be identified.");
                    throw new ServiceException("Could not find club and manager.");
            }

            var entry = mapper.Map<TEntry>(dto);
            
            // Set metadata for new entries.
            entry.SentAt = DateTimeOffset.Now;
            entry.Status = EntryStatus.New;

            return entry;
        }

        /// <summary>
        /// Adds a confirmation email message for the given entry to the emailing queue.
        /// This method takes care of loading referenced data.
        /// </summary>
        /// <param name="entry">Entry for which a confirmation message should be sent.</param>
        /// <returns></returns>
        private async Task QueueEmailConfirmationAsync(Entry entry)
        {
            // Make sure all related data is loaded.
            // TODO: Is this faster or is it faster to just load it again with Include()?
            await db.Entry(entry).Reference(e => e.Club).LoadAsync();
            await db.Entry(entry).Reference(e => e.SentBy).LoadAsync();
            await db.Entry(entry).Reference(e => e.Race).LoadAsync();

            await emailingQueue.QueueConfirmationAsync(entry);
        }

        /// <summary>
        /// Checks if the current user should have access to a given entry record.
        /// Access is granted to all staff members or to managers of the same club.
        /// </summary>
        /// <param name="entry">Entry for which access should be validated.</param>
        /// <returns>True if access should be given, false if not.</returns>
        private async Task<bool> ValidateAccessAsync(Entry entry)
        {
            var user = await GetUserAsync();
            logger.LogDebug("Validating access for user {UserId} to entry {EntryId}.", user.Id, entry.Id);
            return user is StaffMember || user is Manager m && entry.ClubId == m.ClubId;
        }
    }
}
