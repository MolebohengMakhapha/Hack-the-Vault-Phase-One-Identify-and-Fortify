using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FlawedLoginSystem
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }

    public class LoginService
    {
        // Internal storage for users
        private List<User> users = new List<User>();

        public LoginService()
        {
            CreateUser("mo", "password123");
        }

        // CREATE USER: Hash password with salt and store it
        public void CreateUser(string username, string plainPassword)
        {
            string salt = GenerateSalt();
            string hashed = HashPassword(plainPassword, salt);

            users.Add(new User
            {
                Username = username,
                PasswordHash = hashed,
                Salt = salt
            });
        }

        // Check hash of provided password against stored one
        public bool Login(string username, string password)
        {
            var user = users.Find(u => u.Username == username);

            if (user == null) return false;

            string hashed = HashPassword(password, user.Salt);
            return hashed == user.PasswordHash;
        }

        // Random salt
        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        // Hash password with salt using PBKDF2
        private string HashPassword(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); 
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
