using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Model
{
    /// <summary>
    /// Represents a user account with access to the application.
    /// This is a common base class with authentication-related details.
    /// </summary>
    public abstract class User
    {
        public int Id { get; set; } = 0;

        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;

        public IList<RefreshToken> RefreshTokens { get; set; } = null!;
    }
}
