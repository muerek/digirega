using DigiRega.Client.Services;
using DigiRega.Client.Services.Exceptions;
using DigiRega.Shared.Dto.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DigiRega.Client.Auth
{
    class SessionManager : ISessionManager
    {
        private readonly ITokenStorage tokenStorage;
        private readonly IAuthService authService;
        private readonly HttpClient http;
        private readonly AuthenticationStateProvider authenticationState;

        public SessionManager(ITokenStorage tokenStorage, IAuthService authService,
            HttpClient http, AuthenticationStateProvider authenticationState)
        {
            this.tokenStorage = tokenStorage;
            this.authService = authService;
            this.http = http;
            this.authenticationState = authenticationState;
        }

        public async Task<bool> StartAsync(PasswordLoginDto login)
        {
            try
            {
                var token = await authService.LoginWithPasswordAsync(login);
                await CompleteAuthAsync(token);
                return true;
            }
            // Wrong credentials produce an unauthorized error, any other error indicates general failure.
            catch (UnauthorizedServiceException)
            {
                return false;
            }
        }

        public async Task<bool> StartAsync(SecretLoginDto login)
        {
            try
            {
                var token = await authService.LoginWithSecretAsync(login);
                await CompleteAuthAsync(token);
                return true;
            }
            // Wrong credentials produce an unauthorized error, any other error indicates general failure.
            catch (UnauthorizedServiceException)
            {
                return false;
            }
        }

        /// <summary>
        /// Stores the given token, applies it to the default <see cref="HttpClient"/> as bearer token,
        /// and notifies the <see cref="JwtAuthenticationStateProvider"/> to pick up the new authentication details.
        /// </summary>
        /// <param name="token">Access token from login.</param>
        /// <returns></returns>
        private async Task CompleteAuthAsync(GetTokenDto token)
        {
            await tokenStorage.SetAccessTokenAsync(token.AccessToken);
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            (authenticationState as JwtAuthenticationStateProvider)?.NotifyAuthenticationChanged();
        }

        public async Task StopAsync()
        {
            await tokenStorage.RemoveAccessTokenAsync();
            http.DefaultRequestHeaders.Authorization = null;
            (authenticationState as JwtAuthenticationStateProvider)?.NotifyAuthenticationChanged();
        }

        public async Task<bool> TryRestartAsync()
        {
            var oldAccessToken = await tokenStorage.GetAccessTokenAsync();

            // Nothing to do if there is no token.
            if (oldAccessToken == null)
            {
                return false;
            }

            // Outdated and soon-to-be-outdated tokens should not be used and can be removed.
            if (JwtHelper.GetExpiration(oldAccessToken) < DateTime.Now.AddMinutes(3))
            {
                await StopAsync();
                return false;
            }

            var token = new GetTokenDto() { AccessToken = oldAccessToken };
            await CompleteAuthAsync(token);
            return true;
        }
    }
}
