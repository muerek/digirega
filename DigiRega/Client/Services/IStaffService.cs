using DigiRega.Shared.Dto.StaffMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    public interface IStaffService
    {
        Task<IList<GetStaffMemberDto>> GetStaffMembersAsync();

        Task<GetStaffMemberDto> GetStaffMemberAsync(int id);

        Task<int> AddStaffMemberAsync(AddStaffMemberDto staffMember);

        Task UpdateStaffMemberAsync(int id, UpdateStaffMemberDto update);

        Task RemoveStaffMemberAsync(int id);
    }
}
