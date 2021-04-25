using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public interface IClubService
    {
        Task<ServiceResponse<GetClubDto>> GetClubAsync(int id);

        Task<ServiceResponse<IList<GetClubDto>>> GetAllClubsAsync();

        Task<ServiceResponse<int>> AddClubAsync(AddClubDto clubDto);

        Task<ServiceResponse> RemoveClubAsync(int id);

        Task<ServiceResponse> UpdateClubAsync(int id, UpdateClubDto update);
    }
}
