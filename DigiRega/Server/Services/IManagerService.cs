using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public interface IManagerService
    {
        Task<ServiceResponse<GetManagerDto>> GetManagerAsync(int id);

        Task<ServiceResponse<IList<GetManagerDto>>> GetAllManagersAsync(int? clubId = null);

        Task<ServiceResponse<int>> AddManagerAsync(AddManagerDto managerDto);

        Task<ServiceResponse> RemoveManagerAsync(int id);

        Task<ServiceResponse> UpdateManagerAsync(int id, UpdateManagerDto update);
    }
}
