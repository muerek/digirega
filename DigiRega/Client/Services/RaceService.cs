using DigiRega.Client.Services.Exceptions;
using DigiRega.Client.Utilities;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    public class RaceService : IRaceService
    {
        private readonly HttpClient http;
        private readonly string path = "races";

        public RaceService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<int> AddRaceAsync(AddRaceDto race)
        {
            var response = await http.PostAsJsonAsync(path, race);
            await response.EnsureSuccessAsync();
            return await response.Content.ReadFromJsonAsync<ServiceResponse<int>>()
                ?? throw new ServiceException("No data received.");
        }

        public async Task<GetRaceDto> GetRaceAsync(int id)
        {
            try
            {
                var response = await http.GetFromJsonAsync<ServiceResponse<GetRaceDto>>($"{path}/{id}");
                return response?.Data
                    ?? throw new ServiceException("No data received.");
            }
            catch (HttpRequestException e) { throw ServiceException.FromHttpRequestException(e); }
        }

        public async Task<IList<GetRaceDto>> GetRacesAsync()
        {
            try
            {
                var response = await http.GetFromJsonAsync<ServiceResponse<IList<GetRaceDto>>>(path);
                return response?.Data
                    ?? throw new ServiceException("No data received.");
            }
            catch (HttpRequestException e) { throw ServiceException.FromHttpRequestException(e); }
        }

        public async Task RemoveRaceAsync(int id)
        {
            var response = await http.DeleteAsync($"{path}/{id}");
            await response.EnsureSuccessAsync();
        }
    }
}
