using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace c_sharp_BankSystemDevelopment
{
    internal class UserRegistration
    {
        private List<User> registeredUsers = new List<User>();
       
        private void RegisterUser()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter your email: ");
            string email = Console.ReadLine();

            if (IsEmailUnique(email))
            {
                Console.WriteLine("Enter your password: ");
                string inputPassword = Console.ReadLine();
               
                    User newUser = new User(name, email, inputPassword);
                    registeredUsers.Add(newUser);
                    Save();
                    Console.WriteLine("Registration successful!");
               



               
            }
            else
            {
                Console.WriteLine("Email address is already registered.");
            }
        }
        public void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText($"Users.json", json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the invoice: {ex.Message}");
            }
        }

        private bool IsEmailUnique(string email)
        {
            foreach (User user in registeredUsers)
            {
                if (user.Email == email)
                {
                    return false;
                }
            }
            return true;
        }
    }

}

