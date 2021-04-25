using DigiRega.Client.Services;
using DigiRega.Shared.Validation.StaffMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DigiRega.Client.Validation
{
    public class UsernameValidator : IUsernameValidator
    {
        private readonly HttpClient http;
        private readonly IStaffService staffService;

        public UsernameValidator(HttpClient http, IStaffService staffService)
        {
            this.http = http;
            this.staffService = staffService;
        }

        public async Task<bool> CheckUniqueAsync(string username)
        {
            var response = await http.GetAsync($"staff/{username}");
            return !response.IsSuccessStatusCode;
        }
    }
}
