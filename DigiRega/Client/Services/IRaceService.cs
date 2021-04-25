using DigiRega.Shared.Dto.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    public interface IRaceService
    {
        Task<IList<GetRaceDto>> GetRacesAsync();

        Task<GetRaceDto> GetRaceAsync(int id);

        Task<int> AddRaceAsync(AddRaceDto race);

        Task RemoveRaceAsync(int id);
    }
}
