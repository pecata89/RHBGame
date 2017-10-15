using System;
using System.Linq;
using System.Security.Cryptography;

namespace RHBGame.WebApi
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Creates a cryptographically strong (random) salt of the provided size.
        /// </summary>
        public static Byte[] CreateRandomSalt(Int32 size)
        {
            if ( size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            var salt = new Byte[size];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }


        /// <summary>
        /// Computes a 20 bytes hash of the provided password using the provided salt.
        /// </summary>
        public static Byte[] ComputeHash(String password, Byte[] salt)
        {
            using (var crypto = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                return crypto.GetBytes(20);
            }
        }


        /// <summary>
        /// Returns whether the provided password matches the given hash and salt.
        /// </summary>
        public static Boolean CheckHash(String password, Byte[] passwordHash, Byte[] salt)
        {
            return passwordHash.SequenceEqual(ComputeHash(password, salt));
        }
    }
}