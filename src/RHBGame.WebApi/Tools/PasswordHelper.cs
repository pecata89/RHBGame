using System;
using System.Linq;
using System.Security.Cryptography;
using RHBGame.Data.Models;

namespace RHBGame.WebApi
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Creates a cryptographically strong (random) salt of the provided size.
        /// </summary>
        public static Byte[] CreateRandomSalt()
        {
            var salt = new Byte[Player.SaltLength];

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
            if ( salt?.Length != Player.SaltLength)
            {
                throw new ArgumentException("The salt size is not correct.", nameof(salt));
            }

            using (var crypto = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                return crypto.GetBytes(Player.HashLength);
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