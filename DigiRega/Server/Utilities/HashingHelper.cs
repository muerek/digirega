using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Utilities
{
    /// <summary>
    /// Provides utility methods for password hash generation and verification.
    /// </summary>
    public static class HashingHelper
    {
        /// <summary>
        /// Hashes the given password with a random salt.
        /// </summary>
        /// <param name="password">Plain-text password.</param>
        /// <returns>Tuple with hashed password and salt used for hashing.</returns>
        public static (byte[] hash, byte[] salt) CreateHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }

        /// <summary>
        /// Checks a plain-text password against a hashed password value and its salt.
        /// </summary>
        /// <param name="password">Plain-text password.</param>
        /// <param name="hash">Hashed password.</param>
        /// <param name="salt">Salt or HMAC key used for hashing.</param>
        /// <returns>True if the given password matches the hash and salt, false if not.</returns>
        public static bool VerifyHash(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(salt);
            var computed = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computed.Length; i++)
            {
                if (computed[i] != hash[i])
                { return false; }
            }
            return true;
        }
    }
}
