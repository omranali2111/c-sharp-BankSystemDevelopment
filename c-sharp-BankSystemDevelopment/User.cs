using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace c_sharp_BankSystemDevelopment
{
    internal class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            PasswordHash = HashPassword(password);
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
