using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Auth
{
    /// <summary>
    /// Implementation of <see cref="ITokenStorage"/> that uses the local storage of the browser.
    /// </summary>
    public class LocalTokenStorage : ITokenStorage
    {
        private readonly ILocalStorageService localStorage;
        private const string AccessTokenKey = "accessToken";

        public LocalTokenStorage(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task<string?> GetAccessTokenAsync()
        {
            // We could check if the key exists, but that would be an additional call to JavaScript.
            var token = await localStorage.GetItemAsync<string>(AccessTokenKey);
            return string.IsNullOrEmpty(token) ? null : token;
        }

        public async Task RemoveAccessTokenAsync()
        {
            await localStorage.RemoveItemAsync(AccessTokenKey);
        }

        public async Task SetAccessTokenAsync(string token)
        {
            await localStorage.SetItemAsync(AccessTokenKey, token);
        }
    }
}
