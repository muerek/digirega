using AutoMapper;
using DigiRega.Server.Data;
using DigiRega.Server.Model;
using DigiRega.Server.Services.Exceptions;
using DigiRega.Server.Utilities;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public class AuthService : ServiceBase, IAuthService
    {
        private readonly TokenHelper tokenHelper;

        public AuthService(DigiRegaDbContext db, IMapper mapper, IHttpContextAccessor httpContextAccessor,
            TokenHelper tokenHelper, ILogger<AuthService> logger)
            : base(db, mapper, httpContextAccessor, logger)
        {
            this.tokenHelper = tokenHelper;
        }

        public async Task<ServiceResponse<GetTokenDto>> LoginAsync(PasswordLoginDto login)
        {
            logger.LogInformation("User {Username} is trying to log in with password.", login.Username);
            
            var user = await db.Users.SingleOrDefaultAsync(u => u.Username == login.Username);
            if (user == null || !HashingHelper.VerifyHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                logger.LogWarning("Login failed due to wrong credentials for user {Username}.", login.Username);
                // Error message does and should not expose what part of the credentials failed.
                throw new UnauthorizedServiceException("Invalid login credentials.");
            }

            logger.LogInformation("User {Username} logged in successfully with password.", login.Username);
            return GetToken(user);
        }

        public async Task<ServiceResponse<GetTokenDto>> LoginAsync(SecretLoginDto login)
        {
            logger.LogInformation("User is trying to log in with secret.");
            
            // Only managers are assigned a secret, so we can limit the search to the manager data set.
            var user = await db.Managers.SingleOrDefaultAsync(m => m.Secret == login.Secret);
            // No further verification needed, just if something was found or not.
            if (user == null)
            {
                logger.LogWarning("Login failed due to invalid secret.");
                throw new UnauthorizedServiceException("Invalid secret.");
            }

            logger.LogInformation("User logged in successfully with secret.");

            // Load club data for manager. Not using Include() above to avoid large joins.
            // Club data is needed for authorization.
            await db.Entry(user).Reference(u => u.Club).LoadAsync();
            return GetToken(user);
        }

        /// <summary>
        /// Builds an access token for the given user.
        /// </summary>
        /// <param name="user">User to be used as principal for the access token.</param>
        /// <returns>Access token wrapped in a <see cref="GetTokenDto"/>.</returns>
        private GetTokenDto GetToken(User user) => new() { AccessToken = tokenHelper.CreateAccessToken(user) };
    }
}
