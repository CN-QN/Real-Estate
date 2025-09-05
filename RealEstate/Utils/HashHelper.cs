using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace RealEstate.Utils
{
    public static class HashHelper
    {
        private const int SaltSize = 16; // 128 bit
        private const int HashSize = 20; // 160 bit
        private const int Iterations = 10000;

        public static string CreateHash(string password)
        {
            // Generate salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine salt+hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Format: iterations + base64(salt+hash)
            return $"{Iterations}:{Convert.ToBase64String(hashBytes)}";
        }

        public static bool Verify(string password, string storedHash)
        {
            var splitted = storedHash.Split(':');
            var iterations = int.Parse(splitted[0]);
            var hashBytes = Convert.FromBase64String(splitted[1]);

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i]) return false;
            }
            return true;
        }
    }
}