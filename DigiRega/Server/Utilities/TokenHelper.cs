using DigiRega.Server.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Server.Utilities
{
    /// <summary>
    /// Holds a helper method to generate JWT access tokens. 
    /// </summary>
    /// <remarks>
    /// Note that this class has to be instantiated since it accesses the app configuration.
    /// Static classes cannot access the config as it needs to be injected.
    /// </remarks>
    public class TokenHelper
    {
        public TokenHelper(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Creates a JWT access token for the given user.
        /// </summary>
        /// <param name="user">User to create the token for.</param>
        /// <returns>JWT access token.</returns>
        public string CreateAccessToken(User user)
        {
            var credentials = new SigningCredentials(GetJwtSigningKey(), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = GetClaimsIdentity(user),
                Expires = DateTime.Now.AddMinutes(120),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.OutboundClaimTypeMap.Clear();

            return tokenHandler.CreateEncodedJwt(tokenDescriptor);
        }

        /// <summary>
        /// Build a <see cref="ClaimsIdentity"/> for the given user.
        /// Claims will be included based on the user data and user type.
        /// </summary>
        /// <param name="user">User to build a <see cref="ClaimsIdentity"/> for.</param>
        /// <returns><see cref="ClaimsIdentity"/> representing the given user.</returns>
        /// <remarks>
        /// User roles are put into to tokens here based on user type. Not sure if this is a good way in the long run. 
        /// </remarks>
        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            // User ID is a common claim.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            // Set claims for managers.
            if (user is Manager manager)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Manager"));
                claims.Add(new Claim(ClaimTypes.Email, manager.EmailAddress));
                claims.Add(new Claim(ClaimTypes.Name, $"{manager.FirstName} {manager.LastName}"));
                claims.Add(new Claim("ClubId", manager.ClubId.ToString()));
                claims.Add(new Claim("ClubName", manager.Club.Name));
            }

            // Set claims for OC members.
            if (user is StaffMember staffMember)
            {
                claims.Add(new Claim(ClaimTypes.Role, staffMember.Role));
                claims.Add(new Claim(ClaimTypes.Name, staffMember.Username));
            }

            return new ClaimsIdentity(claims);
        }

        private SecurityKey GetJwtSigningKey()
        {
            // TODO: Read up on and switch to asymmetric keys.
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSigningKey"]));
        }
    }
}
