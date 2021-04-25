using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    /// <summary>
    /// Represents a user who is part of the event staff.
    /// </summary>
    public class StaffMember : User
    {
        public string Role { get; set; } = "Staff";
    }
}
