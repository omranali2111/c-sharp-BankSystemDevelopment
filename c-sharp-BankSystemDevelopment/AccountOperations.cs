using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_BankSystemDevelopment
{
    internal class AccountOperations
    {
        private List<Account> accounts = new List<Account>();
       

        public Account CreateAccount(string accountHolderName, decimal initialBalance)
        {
            Account newAccount = new Account(accountHolderName, initialBalance);
            accounts.Add(newAccount);
            return newAccount;
        }

        public void Deposit(Account account, decimal amount)
        {
            if (amount > 0)
            {
                account.Balance += amount;
                Console.WriteLine($"Deposited {amount:RO} into Account {account.AccountNumber}. New balance: {account.Balance:C}");
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
                Console.WriteLine($"Withdrawn {amount:RO} from Account {account.AccountNumber}. New balance: {account.Balance:C}");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount or insufficient balance.");
                return false;
            }
        }
    }
}
