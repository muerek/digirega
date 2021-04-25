using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Shared.Dto.Auth
{
    public class GetTokenDto
    {
        /// <summary>
        /// JWT token issued to the client after authentication for subsequent authorized API calls.
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;
    }
}
