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
        private List<Account> accounts = new List<Account>();
       


        public Account CreateAccount(string accountHolderName, decimal initialBalance)
        {
            Account newAccount = new Account(accountHolderName, initialBalance);
            accounts.Add(newAccount);
            SaveAccountToJson(newAccount);
            return newAccount;

        }
        public void SaveAccountToJson(Account account)
        {
            try
            {
                string fileName = $"{account.AccountNumber}.json";
                string json = JsonSerializer.Serialize(account, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
                Console.WriteLine($"Account data saved to {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving account data: {ex.Message}");
            }
        }
        public void LoadAccountsFromJson(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string json = File.ReadAllText(fileName);
                    List<Account> loadedAccounts = JsonSerializer.Deserialize<List<Account>>(json);
                    if (loadedAccounts != null)
                    {
                        accounts.AddRange(loadedAccounts);
                        Console.WriteLine($"Loaded {loadedAccounts.Count} accounts from {fileName}");
                    }
                }
                else
                {
                    Console.WriteLine($"File {fileName} does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading account data: {ex.Message}");
            }
        }
        public Account GetAccountByNumber(int accountNumber)
        {
            return accounts.FirstOrDefault(account => account.AccountNumber == accountNumber);
        }



        public void Deposit(Account account, decimal amount)
        {
            if (amount > 0)
            {
                account.Balance += amount;
                Console.WriteLine($"Deposited {amount:RO} into Account {account.AccountNumber}. New balance: {account.Balance:RO}");
                SaveAccountToJson(account);
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
                SaveAccountToJson(account);
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
                Deposit(targetAccount,amount);
                Console.WriteLine($"Transferred {amount:RO} from Account {sourceAccount.AccountNumber} to Account {targetAccount.AccountNumber}");

                // Automatically save both accounts after a successful transfer
                SaveAccountToJson(sourceAccount);
                SaveAccountToJson(targetAccount);

                return true;
            }
            else
            {
                Console.WriteLine("Transfer failed.");
                return false;
            }
        }
        public void DisplayAccountInformation(int accountNumber)
        {
            Account account = accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);

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
