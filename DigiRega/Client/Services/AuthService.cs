using DigiRega.Client.Services.Exceptions;
using DigiRega.Client.Utilities;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient http;

        public AuthService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<GetTokenDto> LoginWithPasswordAsync(PasswordLoginDto login)
        {

            var response = await http.PostAsJsonAsync("auth/login-password", login);
            await response.EnsureSuccessAsync();
            return await response.Content.ReadFromJsonAsync<ServiceResponse<GetTokenDto>>()
                ?? throw new ServiceException("No data received.");
        }

        public async Task<GetTokenDto> LoginWithSecretAsync(SecretLoginDto login)
        {
            var response = await http.PostAsJsonAsync("auth/login-secret", login);
            await response.EnsureSuccessAsync();
            return await response.Content.ReadFromJsonAsync<ServiceResponse<GetTokenDto>>()
                ?? throw new ServiceException("No data received.");
        }
    }
}
