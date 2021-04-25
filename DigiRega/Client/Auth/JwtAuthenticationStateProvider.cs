using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DigiRega.Client.Auth
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenStorage tokenStorage;

        public JwtAuthenticationStateProvider(ITokenStorage tokenStorage)
        {
            this.tokenStorage = tokenStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Get the stored access token.
            var accessToken = await tokenStorage.GetAccessTokenAsync();

            ClaimsIdentity identity;
            // Provide identity if an access token is available and it has not expired.
            if(accessToken != null && JwtHelper.GetExpiration(accessToken) > DateTime.Now)
            {
                identity = new ClaimsIdentity(JwtHelper.GetClaims(accessToken), "jwt");
            }
            else
            {
                // Empty claims represent anonymous access.
                identity = new ClaimsIdentity();
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        /// <summary>
        /// Raises an event indicating the authentication state has changed.
        /// This will trigger a full evaluation of the authentication state to pick up any changes.
        /// </summary>
        public void NotifyAuthenticationChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
