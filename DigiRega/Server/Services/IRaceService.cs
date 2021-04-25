using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public interface IRaceService
    {
        Task<ServiceResponse<GetRaceDto>> GetRaceAsync(int id);

        Task<ServiceResponse<IList<GetRaceDto>>> GetAllRacesAsync();

        Task<ServiceResponse<int>> AddRaceAsync(AddRaceDto raceDto);

        Task<ServiceResponse> RemoveRaceAsync(int id);

        Task<ServiceResponse> UpdateRaceAsync(int id, UpdateRaceDto update);
    }
}
