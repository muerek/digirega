using DigiRega.Server.Services;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DigiRega.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        // POST api/<AuthController>/login-secret
        [HttpPost("login-secret")]
        public async Task<ServiceResponse<GetTokenDto>> LoginWithSecretAsync(SecretLoginDto login)
        {
            return await authService.LoginAsync(login);
        }

        // POST api/<AuthController>/login-password
        [HttpPost("login-password")]
        public async Task<ServiceResponse<GetTokenDto>> LoginWithPasswordAsync(PasswordLoginDto login)
        {
            return await authService.LoginAsync(login);
        }


    }
}
