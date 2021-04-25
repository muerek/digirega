using AutoMapper;
using DigiRega.Server.Data;
using DigiRega.Server.Model;
using DigiRega.Server.Services.Exceptions;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Club;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public class ClubService : ServiceBase, IClubService
    {
        public ClubService(DigiRegaDbContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<ClubService> logger)
            : base(db, mapper, httpContextAccessor, logger) { }

        public async Task<ServiceResponse<int>> AddClubAsync(AddClubDto clubDto)
        {
            logger.LogInformation("Adding new club {Name}.", clubDto.Name);
            
            var club = mapper.Map<Club>(clubDto);
            db.Clubs.Add(club);
            await db.SaveChangesAsync();

            logger.LogDebug("Added new club {Name} with ID {Id}.", club.Name, club.Id);
            
            return club.Id;
        }

        public async Task<ServiceResponse<IList<GetClubDto>>> GetAllClubsAsync()
        {
            logger.LogInformation("Getting all clubs.");
            var clubs = await db.Clubs.ToListAsync();
            return mapper.Map<List<GetClubDto>>(clubs);
        }

        public async Task<ServiceResponse<GetClubDto>> GetClubAsync(int id)
        {
            logger.LogInformation("Getting club {Id}.", id);

            var club = await db.Clubs.SingleOrDefaultAsync(c => c.Id == id);
            if (club == null)
            {
                logger.LogWarning("Club {Id} not found.", id);
                throw new NotFoundServiceException($"No club found for ID {id}");
            }

            return mapper.Map<GetClubDto>(club);
        }

        public async Task<ServiceResponse> RemoveClubAsync(int id)
        {
            logger.LogInformation("Removing club {Id}", id);

            var club = await db.Clubs.SingleOrDefaultAsync(c => c.Id == id);
            if (club == null)
            {
                logger.LogWarning("Club {Id} not found.", id);
                throw new NotFoundServiceException($"No club found for ID {id}");
            }

            db.Clubs.Remove(club);
            await db.SaveChangesAsync();

            return SuccessfulServiceResponse();
        }

        public async Task<ServiceResponse> UpdateClubAsync(int id, UpdateClubDto update)
        {
            logger.LogInformation("Updating club {Id}", id);
            
            var club = await db.Clubs.SingleOrDefaultAsync(c => c.Id == id);
            if (club == null)
            {
                logger.LogWarning("Club {Id} not found.", id);
                throw new NotFoundServiceException($"No club found for ID {id}");
            }

            club.Name = update.Name;

            db.Clubs.Update(club);
            await db.SaveChangesAsync();

            return SuccessfulServiceResponse();
        }
    }
}
