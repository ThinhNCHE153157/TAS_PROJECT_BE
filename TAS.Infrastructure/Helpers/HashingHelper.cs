using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using TAS.Infrastructure.Configurations;

namespace TAS.Infrastructure.Helpers
{
    public static class HashingHelper
    {
        /// <summary>
        /// Encrypt password with SHA256 algorithm
        /// </summary>
        /// <param name="originPassword"></param>
        /// <returns>Encrypt password string, default return empty string</returns>
        public static string EncryptPassword(string originPassword)
        {
            if (string.IsNullOrEmpty(originPassword))
            {
                return "";
            }
            byte[] saltkey = Encoding.UTF8.GetBytes(AppSettings.SaltKey);
            var encryptPassword = KeyDerivation.Pbkdf2(
                password: originPassword,
                salt: saltkey,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return Convert.ToHexString(encryptPassword);
        }


        /// <summary>
        /// Compare password encrypt string
        /// </summary>
        /// <param name="originPassword"></param>
        /// <param name="storedPassword"></param>
        /// <returns>True or false</returns>
        public static bool VerifyPassword(string originPassword, string storedPassword)
        {
            string encryptPassword = EncryptPassword(originPassword);
            return storedPassword.Equals(encryptPassword);
        }

    }
}
