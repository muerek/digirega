using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<GetTokenDto>> LoginAsync(PasswordLoginDto login);

        Task<ServiceResponse<GetTokenDto>> LoginAsync(SecretLoginDto login);
    }
}
