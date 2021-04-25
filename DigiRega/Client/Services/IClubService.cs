using DigiRega.Shared.Dto.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    public interface IClubService
    {
        Task<IList<GetClubDto>> GetClubsAsync();

        Task<GetClubDto> GetClubAsync(int id);

        Task<int> AddClubAsync(AddClubDto club);

        Task RemoveClubAsync(int id);
    }
}
