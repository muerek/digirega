using DigiRega.Shared.Dto.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    /// <summary>
    /// Describes a service for handling managers in the backend.
    /// </summary>
    public interface IManagerService
    {
        /// <summary>
        /// Gets a list of all managers.
        /// </summary>
        /// <param name="clubId">Filter managers for the club with this ID.</param>
        /// <returns>List of all managers.</returns>
        /// <exception cref="DigiRega.Client.Services.Exceptions.ServiceException">
        /// Thrown for communication failures or errors with the backend.
        /// </exception>
        Task<IList<GetManagerDto>> GetManagersAsync(int? clubId = null);

        Task<GetManagerDto> GetManagerAsync(int id);

        Task<int> AddManagerAsync(AddManagerDto manager);

        Task UpdateManagerAsync(int id, UpdateManagerDto update);

        Task RemoveManagerAsync(int id);
    }
}
