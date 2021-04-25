using DigiRega.Shared.Definitions;
using DigiRega.Shared.Dto.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    interface IEntryService
    {
        Task<IList<GetEntryBriefDto>> GetEntriesAsync(EntryStatus? status = null);

        Task<GetEntryDto> GetEntryAsync(int id);

        Task AddCrewChangeAsync(AddCrewChangeDto crewChange);

        Task AddLateEntryAsync(AddLateEntryDto lateEntry);

        Task AddWithdrawalAsync(AddWithdrawalDto withdrawal);

        Task UpdateEntryStatusAsync(int id, EntryStatus status);
    }
}
