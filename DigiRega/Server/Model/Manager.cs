using DigiRega.Server.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    /// <summary>
    /// Represents a club manager who can enter entries for their own club.
    /// </summary>
    public class Manager : User
    {         
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Email address used to contact the club manager.
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;

        /// <summary>
        /// Shared secret to authorize access. Keep this safe and limit exposure.
        /// </summary>
        public string Secret { get; set; } = SecretHelper.GenerateSecret();

        public int ClubId { get; set; }
        public virtual Club Club { get; set; } = null!;
    }
}
