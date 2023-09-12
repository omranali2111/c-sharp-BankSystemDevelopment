using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_BankSystemDevelopment
{
    internal class Menu
    {

        private UserRegistration userRegistration;
        private AccountOperations accountOperations;
        private User currentUser;

        public Menu(UserRegistration userRegistration, AccountOperations accountOperations)
        {
            this.userRegistration = userRegistration;
            this.accountOperations = accountOperations;

        }
        public void Start()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Bank System Menu:");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        
                        userRegistration.RegisterUser();
                        break;
                    case "2":
                        bool loginSuccessful = userRegistration.UserLogin();
                        if (loginSuccessful)
                        {
                            currentUser = userRegistration.GetCurrentUser();
                            if (currentUser != null)
                            {
                                Console.WriteLine("Login successful!");
                                AccountMenu();
                            }
                            else
                            {
                                Console.WriteLine("Failed to get the current user.");
                            }
                        }
                        break;
                    case "3":
                        exit = true;
                        Console.WriteLine("Exiting the application. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void AccountMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {currentUser.Name} ({currentUser.Email})");
                Console.WriteLine("Account Operations:");
                Console.WriteLine("1. Create New Account");
                Console.WriteLine("2. Enter Account");
                Console.WriteLine("3. Logout");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateNewAccount();
                        break;
                    case "2":
                        EnterAccountSubMenu();
                        break;                 
                    case "3":
                        currentUser = null; // Logout the current user
                        exit = true;
                        Console.WriteLine("Logged out successfully.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        private void CreateNewAccount()
        {
            Console.WriteLine("Enter account holder name: ");
            string accountHolderName = Console.ReadLine();

            Console.WriteLine("Enter initial balance: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
            {
                accountOperations.CreateAccount(currentUser, accountHolderName, initialBalance);

                Console.WriteLine("Account creation initiated.");
            }
            else
            {
                Console.WriteLine("Invalid initial balance. Please enter a valid number.");
            }
        }
        private void EnterAccountSubMenu()
        {
            Console.WriteLine("Enter account number: ");
            if (int.TryParse(Console.ReadLine(), out int accountNumber))
            {
                bool exit = false;
                var userAccounts = accountOperations.GetAccountsForUser(currentUser);
                Account selectedAccount = userAccounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);

                if (selectedAccount != null)
                {
                    while (!exit)
                    {
                        Console.Clear();
                        Console.WriteLine($"Account Number: {selectedAccount.AccountNumber}");
                        Console.WriteLine($"Account Holder Name: {selectedAccount.AccountHolderName}");
                        Console.WriteLine($"Current Balance: {selectedAccount.Balance:RO}");
                        Console.WriteLine("Account Operations:");
                        Console.WriteLine("1. Deposit");
                        Console.WriteLine("2. Withdraw");
                        Console.WriteLine("3. Money Transfer");
                        Console.WriteLine("4. Back to Main Menu");

                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                DepositOperation(selectedAccount);
                                break;
                            case "2":
                                WithdrawalOperation(selectedAccount);
                                break;
                            case "3":
                                MoneyTransferOperation(selectedAccount);
                                break;
                            case "4":
                                exit = true; // Return to the main account menu
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid account number. Please enter a valid number.");
            }
        }

        private void DepositOperation(Account account)
        {
            Console.WriteLine("Enter the amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                accountOperations.Deposit(account, amount);
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a valid number.");
            }
        }
        private void WithdrawalOperation(Account account)
        {
            Console.WriteLine("Enter the amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                accountOperations.Withdraw(account, amount);
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a valid number.");
            }
        }
        private void MoneyTransferOperation(Account sourceAccount)
        {
            Console.WriteLine("Enter the recipient's account number: ");
            if (int.TryParse(Console.ReadLine(), out int recipientAccountNumber))
            {
                var userAccounts = accountOperations.GetAccountsForUser(currentUser);
                Account recipientAccount = userAccounts.FirstOrDefault(acc => acc.AccountNumber == recipientAccountNumber);

                if (recipientAccount != null)
                {
                    Console.WriteLine("Enter the amount to transfer: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        accountOperations.Transfer(sourceAccount, recipientAccount, amount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Recipient account not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid recipient account number. Please enter a valid number.");
            }
        }



    }
}
    

