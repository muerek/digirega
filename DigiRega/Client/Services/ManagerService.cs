using DigiRega.Client.Services.Exceptions;
using DigiRega.Client.Utilities;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Manager;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    public class ManagerService : IManagerService
    {
        private readonly HttpClient http;
        private readonly string path = "managers";

        public ManagerService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<int> AddManagerAsync(AddManagerDto manager)
        {
            var response = await http.PostAsJsonAsync(path, manager);
            await response.EnsureSuccessAsync();
            return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>()
                ?? throw new ServiceException("No data received.");
        }

        public async Task<GetManagerDto> GetManagerAsync(int id)
        {
            try
            {
                var response = await http.GetFromJsonAsync<ServiceResponse<GetManagerDto>>($"{path}/{id}");
                return response?.Data
                    ?? throw new ServiceException("No data received.");
            }
            catch (HttpRequestException e) { throw ServiceException.FromHttpRequestException(e); }
        }

        public async Task<IList<GetManagerDto>> GetManagersAsync(int? clubId = null)
        {
            var uri = path;

            if (clubId != null)
            {
                uri = QueryHelpers.AddQueryString(uri, "club", clubId.ToString());
            }

            try
            {
                var response = await http.GetFromJsonAsync<ServiceResponse<IList<GetManagerDto>>>(uri);
                return response?.Data
                    ?? throw new ServiceException("No data received.");
            }
            catch (HttpRequestException e) { throw ServiceException.FromHttpRequestException(e); }
        }

        public async Task RemoveManagerAsync(int id)
        {
            var response = await http.DeleteAsync($"{path}/{id}");
            await response.EnsureSuccessAsync();
        }

        public async Task UpdateManagerAsync(int id, UpdateManagerDto update)
        {
            var response = await http.PutAsJsonAsync($"{path}/{id}", update);
            await response.EnsureSuccessAsync();
        }
    }
}
