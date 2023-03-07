using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.DataProvider
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;

        public static string HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with the salt using PBKDF2
            byte[] hash = new Rfc2898DeriveBytes(password, salt, 10000).GetBytes(KeySize);

            // Combine the salt and hash into a single string
            byte[] hashBytes = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Extract the salt and hash from the hashed password
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);
            byte[] hash = new byte[KeySize];
            Array.Copy(hashBytes, SaltSize, hash, 0, KeySize);

            // Hash the entered password with the salt using PBKDF2
            byte[] enteredHash = new Rfc2898DeriveBytes(password, salt, 10000).GetBytes(KeySize);

            // Compare the hashes
            for (int i = 0; i < KeySize; i++)
            {
                if (hash[i] != enteredHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
