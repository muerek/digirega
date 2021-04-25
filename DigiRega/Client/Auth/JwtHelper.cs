using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigiRega.Client.Auth
{
    /// <summary>
    /// Holds methods to read data from JWT tokens.
    /// </summary>
    public static class JwtHelper
    {
        /// <summary>
        /// Parses a JWT token and extracts the claims from it.
        /// </summary>
        /// <param name="jwt">JWT token to be parsed.</param>
        /// <returns>Claims contained in the JWT token.</returns>
        public static IEnumerable<Claim> GetClaims(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? ""));
        }

        /// <summary>
        /// Extracts the expiration date from a given JWT token.
        /// </summary>
        /// <param name="jwt">JWT token to be parsed.</param>
        /// <returns>Expiration as <see cref="DateTime"/> in local time. You can compare against <see cref="DateTime.Now"/> safely.</returns>
        public static DateTime GetExpiration(string jwt)
        {
            var claims = GetClaims(jwt);
            var expirationClaim = claims.SingleOrDefault(c => c.Type == "exp");

            // Tokens without expiration should fail immediately.
            if (expirationClaim == null) { throw new ArgumentException("Could not read expiration from token.", nameof(jwt)); }

            var expirationDate = new DateTime(1970, 1, 1).AddSeconds(double.Parse(expirationClaim.Value)).ToLocalTime();
            return expirationDate;
        }

        /// <summary>
        /// Helper method to parse base64-encoded data.
        /// </summary>
        /// <param name="base64">Base64-encoded data.</param>
        /// <returns>Parsed data as byte array.</returns>
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}