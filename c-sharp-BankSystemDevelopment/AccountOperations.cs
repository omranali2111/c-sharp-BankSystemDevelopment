using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace c_sharp_BankSystemDevelopment
{
    internal class AccountOperations
    {
        private Dictionary<User, List<Account>> userAccounts = new Dictionary<User, List<Account>>();

        public void CreateAccount(User user, string accountHolderName, decimal initialBalance)
        {
            LoadUserAccountsFromJson(); // Load user accounts from JSON

            if (!userAccounts.ContainsKey(user))
            {
                userAccounts[user] = new List<Account>();
            }

            Account newAccount = new Account(accountHolderName, initialBalance);
            userAccounts[user].Add(newAccount);

            SaveUserAccountsToJson(); // Save user accounts to JSON

            Console.WriteLine($"Account created successfully. Account number: {newAccount.AccountNumber}");
        }

        public List<Account> GetAccountsForUser(User user)
        {
            if (userAccounts.ContainsKey(user))
            {
                return userAccounts[user];
            }
            else
            {
                Console.WriteLine("User not found.");
                return new List<Account>();
            }
        }



        public void SaveUserAccountsToJson()
        {
            try
            {
                string fileName = "UserAccounts.json";
                string json = JsonSerializer.Serialize(userAccounts, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
                Console.WriteLine("User-account associations saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving user-account associations: {ex.Message}");
                
            }
        }


        public void LoadUserAccountsFromJson()
        {
            try
            {
                string fileName = "UserAccounts.json";
                if (File.Exists(fileName))
                {
                    string json = File.ReadAllText(fileName);
                    userAccounts = JsonSerializer.Deserialize<Dictionary<User, List<Account>>>(json);
                    Console.WriteLine($"Loaded user-account associations from {fileName}");
                }
                else
                {
                    Console.WriteLine($"File {fileName} does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading user-account associations: {ex.Message}");
            }
        }


        public void Deposit(Account account, decimal amount)
        {
            if (amount > 0)
            {
                account.Balance += amount;
                Console.WriteLine($"Deposited {amount:RO} into Account {account.AccountNumber}. New balance: {account.Balance:RO}");
                SaveUserAccountsToJson();
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
            }
        }

        public bool Withdraw(Account account, decimal amount)
        {
            if (amount > 0 && amount <= account.Balance)
            {
                account.Balance -= amount;
                Console.WriteLine($"Withdrawn {amount:RO} from Account {account.AccountNumber}. New balance: {account.Balance:RO}");
                SaveUserAccountsToJson();
                return true;
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount or insufficient balance.");
                return false;
            }
        }
        public bool Transfer(Account sourceAccount, Account targetAccount, decimal amount)
        {
            if (Withdraw(sourceAccount, amount))
            {
                Deposit(targetAccount, amount);
                Console.WriteLine($"Transferred {amount:RO} from Account {sourceAccount.AccountNumber} to Account {targetAccount.AccountNumber}");

                // Save user-account associations here after a successful transfer
                SaveUserAccountsToJson();

                return true; // Transfer succeeded
            }
            else
            {
                Console.WriteLine("Transfer failed.");
                return false; // Transfer failed
            }
        }


        public void DisplayAccountInformation(User user, int accountNumber)
        {
            List<Account> userAccounts = GetAccountsForUser(user);
            Account account = userAccounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);

            if (account != null)
            {
                Console.WriteLine($"Account Number: {account.AccountNumber}");
                Console.WriteLine($"Account Holder Name: {account.AccountHolderName}");
                Console.WriteLine($"Current Balance: {account.Balance:Ro}");
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

    }
}
