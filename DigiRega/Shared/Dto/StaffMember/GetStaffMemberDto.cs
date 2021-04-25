using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.StaffMember
{
    public class GetStaffMemberDto
    {
        public int Id { get; set; } = 0;

        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = "Staff";
    }
}
