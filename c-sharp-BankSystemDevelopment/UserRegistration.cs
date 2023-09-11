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
       
        private User currentUser;

        public void RegisterUser()
        {
            
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter your email: ");
            string email = Console.ReadLine();
          
            if (IsEmailUnique(email))
            {
                Console.WriteLine("Enter your password: ");
                string inputPassword = Console.ReadLine();
                LoadUsersFromJson();
                User newUser = new User(name, email, inputPassword);
                registeredUsers.Add(newUser);
                SaveUsersToJson();
                Console.WriteLine("Registration successful!");
                Thread.Sleep(1000);
               
            }
            else
            {
                Console.WriteLine("Email address is already registered.");
            }
        }
        public void SaveUsersToJson()
        {
            try
            {
                string json = JsonSerializer.Serialize(registeredUsers, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("Users.json", json);
                Console.WriteLine("User data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving user data: {ex.Message}");
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
        public void LoadUsersFromJson()
        {
            try
            {
                string json = File.ReadAllText("Users.json");
                List<User> loadedUsers = JsonSerializer.Deserialize<List<User>>(json);
                if (loadedUsers != null)
                {
                    
                    registeredUsers.AddRange(loadedUsers); // Add loaded users to the list
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading user data: {ex.Message}");
            }
        }




        public bool LoginUser(string email, string password)
        {
            LoadUsersFromJson();

            User user = registeredUsers.FirstOrDefault(u => u.Email == email);

            if (user != null && user.VerifyPassword(password))
            {
                Console.WriteLine("Login successful!");
                return true;
            }
            else
            {
                Console.WriteLine("Login failed. Check your email and password.");
                return false;
            }
        }
        public bool UserLogin()
        {
            
            Console.WriteLine("Enter your email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Enter your password: ");
            string password = Console.ReadLine();
         

            bool loginSuccessful = LoginUser(email, password);

            if (loginSuccessful)
            {
                currentUser = registeredUsers.FirstOrDefault(u => u.Email == email);
            }

            return loginSuccessful;
        }
        public User GetCurrentUser()
        {
            return currentUser; 
        }
       
    }

}

