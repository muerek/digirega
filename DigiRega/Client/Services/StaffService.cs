using DigiRega.Client.Services.Exceptions;
using DigiRega.Client.Utilities;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.StaffMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    public class StaffService : IStaffService
    {
        private readonly HttpClient http;
        private readonly string path = "staff";

        public StaffService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<int> AddStaffMemberAsync(AddStaffMemberDto staffMember)
        {
            var response = await http.PostAsJsonAsync(path, staffMember);
            await response.EnsureSuccessAsync();
            return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>()
                ?? throw new ServiceException("No data received.");
        }

        public async Task<GetStaffMemberDto> GetStaffMemberAsync(int id)
        {
            try
            {
                var response = await http.GetFromJsonAsync<ServiceResponse<GetStaffMemberDto>>($"{path}/{id}");
                return response?.Data
                    ?? throw new ServiceException("No data received.");
            }
            catch (HttpRequestException e) { throw ServiceException.FromHttpRequestException(e); }
        }

        public async Task<IList<GetStaffMemberDto>> GetStaffMembersAsync()
        {
            try
            {
                var response = await http.GetFromJsonAsync<ServiceResponse<IList<GetStaffMemberDto>>>(path);
                return response?.Data
                    ?? throw new ServiceException("No data received.");
            }
            catch (HttpRequestException e) { throw ServiceException.FromHttpRequestException(e); }
        }

        public async Task RemoveStaffMemberAsync(int id)
        {
            var response = await http.DeleteAsync($"{path}/{id}");
            await response.EnsureSuccessAsync();
        }

        public async Task UpdateStaffMemberAsync(int id, UpdateStaffMemberDto update)
        {
            var response = await http.PutAsJsonAsync($"{path}/{id}", update);
            await response.EnsureSuccessAsync();
        }
    }
}
