using DigiRega.Shared.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Auth
{
    /// <summary>
    /// Describes a service maintaining authentication sessions.
    /// This includes logging in and retrieving authentication information,
    /// storing this information on the client side and applying it to API calls.
    /// </summary>
    interface ISessionManager
    {
        /// <summary>
        /// Starts a session using the given login credentials.
        /// With valid credentials, an access token is retrieved, stored and applied to subsequent API calls.
        /// </summary>
        /// <param name="login">Login credentials containing username and password.</param>
        /// <returns>True if session could be started, false if not.</returns>
        Task<bool> StartAsync(PasswordLoginDto login);

        /// <summary>
        /// Starts a session using the given login credentials.
        /// With valid credentials, an access token is retrieved, stored and applied to subsequent API calls.
        /// </summary>
        /// <param name="login">Login credentials containing a secret.</param>
        /// <returns>True if session could be started, false if not.</returns>
        Task<bool> StartAsync(SecretLoginDto login);

        /// <summary>
        /// Stops the current session and removes all stored authentication information.
        /// </summary>
        /// <returns></returns>
        Task StopAsync();

        /// <summary>
        /// Tries to resume a previous session by checking for stored authentication information.
        /// If there is any and they are still valid, it will be used.
        /// If there is outdated information, it will be cleaned up to avoid confusion.
        /// </summary>
        /// <returns>True if a previous session could be resumed, false if not.</returns>
        Task<bool> TryRestartAsync();
    }
}
