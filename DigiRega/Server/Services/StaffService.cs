using AutoMapper;
using DigiRega.Server.Data;
using DigiRega.Server.Model;
using DigiRega.Server.Services.Exceptions;
using DigiRega.Server.Utilities;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.StaffMember;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public class StaffService : ServiceBase, IStaffService
    {
        public StaffService(DigiRegaDbContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<StaffService> logger)
            : base(db, mapper, httpContextAccessor, logger) { }


        public async Task<ServiceResponse<int>> AddStaffMemberAsync(AddStaffMemberDto staffMemberDto)
        {
            logger.LogInformation("Adding new staff member {Username}", staffMemberDto.Username);

            var staffMember = mapper.Map<StaffMember>(staffMemberDto);
            (staffMember.PasswordHash, staffMember.PasswordSalt) = HashingHelper.CreateHash(staffMemberDto.Password);

            db.Staff.Add(staffMember);
            await db.SaveChangesAsync();

            logger.LogDebug("Added new staff member {Username} with ID {Id}.", staffMember.Username, staffMember.Id);

            return staffMember.Id;
        }

        public async Task<ServiceResponse<IList<GetStaffMemberDto>>> GetAllStaffAsync()
        {
            logger.LogInformation("Getting all staff members.");
            var staff = await db.Staff.ToListAsync();
            return mapper.Map<List<GetStaffMemberDto>>(staff);
        }

        public async Task<ServiceResponse<GetStaffMemberDto>> GetStaffMemberAsync(int id)
        {
            logger.LogInformation("Getting staff member {Id}.", id);
            
            var staffMember = await db.Staff.SingleOrDefaultAsync(s => s.Id == id);
            if (staffMember == null)
            {
                logger.LogWarning("Staff member {Id} not found.", id);
                throw new NotFoundServiceException($"No staff member found for ID {id}.");
            }
            
            return mapper.Map<GetStaffMemberDto>(staffMember);
        }

        public async Task<ServiceResponse<GetStaffMemberDto>> GetStaffMemberAsync(string username)
        {
            logger.LogInformation("Getting staff member {Username}.", username);

            var staffMember = await db.Staff.SingleOrDefaultAsync(s => s.Username == username);
            if (staffMember == null)
            {
                logger.LogWarning("Staff member {Username} not found.", username);
                throw new NotFoundServiceException($"No staff member found for username {username}.");
            }

            return mapper.Map<GetStaffMemberDto>(staffMember);
        }

        public async Task<ServiceResponse> RemoveStaffMemberAsync(int id)
        {
            logger.LogInformation("Removing staff member {Id}.", id);
            
            var staffMember = await db.Staff.SingleOrDefaultAsync(s => s.Id == id);
            if (staffMember == null)
            {
                logger.LogWarning("Staff member {Id} not found.", id);
                throw new NotFoundServiceException($"No staff member found for ID {id}.");
            }

            // Do not remove admin user if there are no other admin users.
            if (staffMember.Role == "Admin" && (await db.Staff.CountAsync(s => s.Role == "Admin")) == 1)
            {
                logger.LogWarning("Cannot remove staff member {Id} as they are the only admin.", staffMember.Id);
                throw new ServiceException("Cannot remove admin, please add another admin first.");
            }

            db.Staff.Remove(staffMember);
            await db.SaveChangesAsync();

            return SuccessfulServiceResponse();
        }

        public async Task<ServiceResponse> UpdateStaffMemberAsync(int id, UpdateStaffMemberDto update)
        {
            logger.LogInformation("Updating staff member {Id}", id);

            var staffMember = await db.Staff.SingleOrDefaultAsync(s => s.Id == id);
            if (staffMember == null)
            {
                logger.LogWarning("Staff member {Id} not found.", id);
                throw new NotFoundServiceException($"No staff member found for ID {id}.");
            }

            // Do not remove admin role if this user is the last remaining admin.
            if (staffMember.Role == "Admin" && update.Role != "Admin" && (await db.Staff.CountAsync(s => s.Role == "Admin")) == 1)
            {
                logger.LogWarning("Cannot remove admin rights from staff member {Id} as they are the only admin.", staffMember.Id);
                throw new ServiceException("Cannot remove admin rights, please add another admin first.");
            }

            // Update role.
            staffMember.Role = update.Role;
            
            // Password is optional in an update and may be set to null if no update is required.
            if (update.Password != null)
            {
                logger.LogDebug("Updating staff member {Id} with new password.", id);
                (staffMember.PasswordHash, staffMember.PasswordSalt) = HashingHelper.CreateHash(update.Password);
            }

            db.Staff.Update(staffMember);
            await db.SaveChangesAsync();

            return SuccessfulServiceResponse();
        }
    }
}
