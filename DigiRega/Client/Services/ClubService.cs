using DigiRega.Client.Services.Exceptions;
using DigiRega.Client.Utilities;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Club;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    public class ClubService : IClubService
    {
        private readonly HttpClient http;
        private readonly string path = "clubs";

        public ClubService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<int> AddClubAsync(AddClubDto club)
        {
            var response = await http.PostAsJsonAsync(path, club);
            await response.EnsureSuccessAsync();
            return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>()
                    ?? throw new ServiceException("No data received.");
        }

        public async Task<GetClubDto> GetClubAsync(int id)
        {
            try
            {
                var response = await http.GetFromJsonAsync<ServiceResponse<GetClubDto>>($"{path}/{id}");
                return response?.Data
                    ?? throw new ServiceException("No data received.");
            }
            catch (HttpRequestException e) { throw ServiceException.FromHttpRequestException(e); }
        }

        public async Task<IList<GetClubDto>> GetClubsAsync()
        {
            try
            {
                var response = await http.GetFromJsonAsync<ServiceResponse<IList<GetClubDto>>>(path);
                return response?.Data
                    ?? throw new ServiceException("No data received.");
            }
            catch (HttpRequestException e) { throw ServiceException.FromHttpRequestException(e); }
        }

        public async Task RemoveClubAsync(int id)
        {
            var response = await http.DeleteAsync($"{path}/{id}");
            await response.EnsureSuccessAsync();
        }
    }
}
