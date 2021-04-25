using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    interface IAuthService
    {
        Task<GetTokenDto> LoginWithPasswordAsync(PasswordLoginDto login);

        Task<GetTokenDto> LoginWithSecretAsync(SecretLoginDto login);
    }
}
