using DigiRega.Client.Services.Exceptions;
using DigiRega.Client.Utilities;
using DigiRega.Shared.Definitions;
using DigiRega.Shared.Dto;
using DigiRega.Shared.Dto.Entry;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigiRega.Client.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient http;
        private readonly string path = "entries";

        public EntryService(HttpClient http)
        {
            this.http = http;
        }

        public async Task AddCrewChangeAsync(AddCrewChangeDto crewChange)
        {
            var response = await http.PostAsJsonAsync($"{path}/crew-changes", crewChange);
            await response.EnsureSuccessAsync();
        }

        public async Task AddLateEntryAsync(AddLateEntryDto lateEntry)
        {
            var response = await http.PostAsJsonAsync($"{path}/late-entries", lateEntry);
            await response.EnsureSuccessAsync();
        }

        public async Task AddWithdrawalAsync(AddWithdrawalDto withdrawal)
        {
            var response = await http.PostAsJsonAsync($"{path}/withdrawals", withdrawal);
            await response.EnsureSuccessAsync();
        }

        public async Task<IList<GetEntryBriefDto>> GetEntriesAsync(EntryStatus? status = null)
        {
            var uri = path;

            if (status != null)
            {
                uri = QueryHelpers.AddQueryString(uri, "status", status.ToString());
            }

            try
            {
                var response = await http.GetFromJsonAsync<ServiceResponse<IList<GetEntryBriefDto>>>(uri);
                return response?.Data
                    ?? throw new ServiceException("No data received.");
            }
            catch (HttpRequestException e) { throw ServiceException.FromHttpRequestException(e); }
        }

        public async Task<GetEntryDto> GetEntryAsync(int id)
        {
            try
            {
                var response = await http.GetFromJsonAsync<ServiceResponse<GetEntryDto>>($"{path}/{id}");
                return response?.Data
                    ?? throw new ServiceException("No data received.");
            }
            catch (HttpRequestException e) { throw ServiceException.FromHttpRequestException(e); }
        }

        public async Task UpdateEntryStatusAsync(int id, EntryStatus status)
        {
            var update = new UpdateEntryStatusDto() { Status = status };

            var response = await http.PutAsJsonAsync($"{path}/{id}/status", update);
            await response.EnsureSuccessAsync();
        }
    }
}
