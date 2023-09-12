using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BCrypt.Net;
using System.IO; 
using System.Threading; 


namespace c_sharp_BankSystemDevelopment
{
    internal class User
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("PasswordHash")]
        public string PasswordHash { get; set; }

        
        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }


       

       
    }
}
