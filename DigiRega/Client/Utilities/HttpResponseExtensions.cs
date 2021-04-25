using DigiRega.Client.Services.Exceptions;
using DigiRega.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DigiRega.Client.Utilities
{
    public static class HttpResponseExtensions
    {
        /// <summary>
        /// Throws a <see cref="ServiceException"/> if <see cref="HttpResponseMessage.IsSuccessStatusCode"/> does not indicate success.
        /// The <see cref="ServiceException"/> will include the content of the <paramref name="response"/> as its message.
        /// </summary>
        /// <param name=""></param>
        public static async Task EnsureSuccessAsync(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                // Try to read a message from the usual wrapper.
                var serviceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse>();
                var message = serviceResponse?.Message ?? await response.Content.ReadAsStringAsync();
                throw ServiceException.FromHttpStatus(response.StatusCode, message);
            }
        }

    }
}
