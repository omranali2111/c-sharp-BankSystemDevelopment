using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO; 
using System.Threading; 


namespace c_sharp_BankSystemDevelopment
{
    internal class UserRegistration
    {
        public List<User> registeredUsers = new List<User>();
        public bool loginSuccessful;
        public User currentUser;

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
        public void LoadUsersFromJson()
        {
            try
            {
                if (File.Exists("Users.json"))
                {
                    string json = File.ReadAllText("Users.json");
                    registeredUsers = JsonSerializer.Deserialize<List<User>>(json);
                    Console.WriteLine("User data loaded successfully.");
                }
                else
                {
                    Console.WriteLine("File 'Users.json' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading user data: {ex.Message}");
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





        public bool LoginUser(string email, string password)
        {
            LoadUsersFromJson();

            User user = registeredUsers.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                Console.WriteLine($"Loaded user: {user.Name}, {user.Email}, {user.PasswordHash}");
                if (user.PasswordHash==password)
                {
                    Console.WriteLine("Login successful!");
                    return true;
                }
                else
                {
                    Console.WriteLine("Password verification failed. Incorrect password.");
                }
            }
            else
            {
                Console.WriteLine($"User with email '{email}' not found.");
            }

            Console.WriteLine("Login failed. Check your email and password.");
            return false;
        }


        public bool UserLogin()
        {
            LoadUsersFromJson();
            Console.WriteLine("Enter your email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Enter your password: ");
            string password = Console.ReadLine();

            
            
            loginSuccessful = LoginUser(email, password); // Set the class-level variable

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

