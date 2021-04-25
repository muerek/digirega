using AutoMapper;
using DigiRega.Server.Data;
using DigiRega.Server.Model;
using DigiRega.Server.Services.Exceptions;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Race;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public class RaceService : ServiceBase, IRaceService
    {
        public RaceService(DigiRegaDbContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<RaceService> logger)
            : base(db, mapper, httpContextAccessor, logger) { }

        public async Task<ServiceResponse<int>> AddRaceAsync(AddRaceDto raceDto)
        {
            logger.LogInformation("Adding new race ({Number}) {ShortDescription}.", raceDto.Number, raceDto.Name);

            var race = mapper.Map<Race>(raceDto);
            db.Races.Add(race);
            await db.SaveChangesAsync();

            logger.LogDebug("Added new race ({Number}) {ShortDescription} with ID {Id}.", race.Number, race.Name, race.Id);

            return race.Id;
        }

        public async Task<ServiceResponse<IList<GetRaceDto>>> GetAllRacesAsync()
        {
            logger.LogInformation("Getting all races.");
            var races = await db.Races.ToListAsync();
            return mapper.Map<List<GetRaceDto>>(races);
        }

        public async Task<ServiceResponse<GetRaceDto>> GetRaceAsync(int id)
        {
            logger.LogInformation("Getting race {Id}.", id);

            var race = await db.Races.SingleOrDefaultAsync(r => r.Id == id);
            if (race == null)
            {
                logger.LogWarning("Race {Id} not found.", id);
                throw new NotFoundServiceException($"No race found for ID {id}");
            }

            return mapper.Map<GetRaceDto>(race);
        }

        public async Task<ServiceResponse> RemoveRaceAsync(int id)
        {
            logger.LogInformation("Removing race {Id}.", id);
            
            var race = await db.Races.SingleOrDefaultAsync(r => r.Id == id);
            if (race == null)
            {
                logger.LogWarning("Race {Id} not found.", id);
                throw new NotFoundServiceException($"No race found for ID {id}");
            }
            
            db.Races.Remove(race);
            await db.SaveChangesAsync();

            return SuccessfulServiceResponse();
        }

        public async Task<ServiceResponse> UpdateRaceAsync(int id, UpdateRaceDto update)
        {
            logger.LogInformation("Removing race {Id}.", id);

            var race = await db.Races.SingleOrDefaultAsync(r => r.Id == id);
            if (race == null)
            {
                logger.LogWarning("Race {Id} not found.", id);
                throw new NotFoundServiceException($"No race found for ID {id}");
            }

            race.Number = update.Number;
            race.Name = update.Name;

            db.Races.Update(race);
            await db.SaveChangesAsync();

            return SuccessfulServiceResponse();
        }
    }
}
