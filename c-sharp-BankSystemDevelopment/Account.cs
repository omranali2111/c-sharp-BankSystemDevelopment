using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_sharp_BankSystemDevelopment
{
    internal class Account
    {
        private static int nextAccountNumber = 1;

        public int AccountNumber { get; }
        public string AccountHolderName { get; }
        public decimal Balance { get;  set; }

        public Account(string accountHolderName, decimal initialBalance)
        {
            AccountNumber = nextAccountNumber++;
            AccountHolderName = accountHolderName;
            Balance = initialBalance;
        }
    }
}
