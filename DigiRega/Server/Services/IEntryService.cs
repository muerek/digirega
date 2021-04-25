using DigiRega.Server.Utilities;
using DigiRega.Shared.Definitions;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public interface IEntryService
    {
        public Task<ServiceResponse<GetEntryDto>> GetEntryAsync(int id);

        public Task<ServiceResponse<IList<GetEntryBriefDto>>> GetAllEntriesAsync(EntryStatus? status = null);

        public Task<ServiceResponse<int>> AddCrewChangeAsync(AddCrewChangeDto crewChangeDto);

        public Task<ServiceResponse<int>> AddLateEntryAsync(AddLateEntryDto lateEntryDto);

        public Task<ServiceResponse<int>> AddWithdrawalAsync(AddWithdrawalDto withdrawalDto);

        public Task<ServiceResponse> UpdateStatusAsync(int id, UpdateEntryStatusDto statusUpdate);
    }
}
