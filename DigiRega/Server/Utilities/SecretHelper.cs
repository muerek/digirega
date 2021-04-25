using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DigiRega.Server.Utilities
{
    /// <summary>
    /// Holds a helper method to generate random secrets.
    /// </summary>
    public static class SecretHelper
    {
        /// <summary>
        /// Generates a random string to be used as a secret.
        /// The secret is base64url-encoded so it can be used in an URL.
        /// </summary>
        /// <param name="bytes">Byte length of the secret.</param>
        /// <returns>Base64url-encoded string with random content.</returns>
        public static string GenerateSecret(int bytes = 64)
        {
            var random = new byte[bytes];
            using var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(random);
            return WebEncoders.Base64UrlEncode(random);
        }

    }
}
