using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Auth
{
    /// <summary>
    /// Represents a general storage for tokens. Tokens can be retrieved, stored and removed.
    /// </summary>
    public interface ITokenStorage
    {
        /// <summary>
        /// Gets the stored access token if available.
        /// </summary>
        /// <returns>Stored access token or null if none available.</returns>
        Task<string?> GetAccessTokenAsync();

        /// <summary>
        /// Removes any access token from the storage.
        /// </summary>
        /// <returns></returns>
        Task RemoveAccessTokenAsync();

        /// <summary>
        /// Saves an access token to the storage, overwriting any previously saved token.
        /// </summary>
        /// <param name="token">Access token to be stored.</param>
        /// <returns></returns>
        Task SetAccessTokenAsync(string token);
    }
}
