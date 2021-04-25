using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.StaffMember
{
    /// <summary>
    /// Holds data on an update to a staff member.
    /// </summary>
    public class UpdateStaffMemberDto
    {
        /// <summary>
        /// New password for logging into the application.
        /// Set to null if password should not be updated.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Password { get; set; } = null;

        /// <summary>
        /// New staff member role.
        /// </summary>
        public string Role { get; set; } = "Staff";
    }
}
