using System;
using System.Collections.Generic;
using System.Linq;
using Task02.BankAccountSystem.Models;

namespace Task02.BankAccountSystem.Services
{
    public class BankService
    {
        private List<BankAccount> accounts = new List<BankAccount>();
        private List<Customer> customers = new List<Customer>();
        private int nextCustomerId = 1;
        private int nextAccountNumber = 1000;

        public void CreateAccount()
        {
            Console.WriteLine("Enter your Full Name ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your Email ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your Phone Number ");
            string phone = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Invalid Data. Name, Email and Phone are required.");
                return;
            }

            Console.WriteLine("Enter Initial Balance ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal initialBalance))
            {
                Console.WriteLine("Invalid Balance.");
                return;
            }

            if (initialBalance < 0)
            {
                Console.WriteLine("Initial balance cannot be negative.");
                return;
            }

            Console.WriteLine("Choose Account Type (1-Savings, 2-Current)");
            string typeInput = Console.ReadLine();
            AccountType accountType;
            if (typeInput == "1")
                accountType = AccountType.Savings;
            else if (typeInput == "2")
                accountType = AccountType.Current;
            else
            {
                Console.WriteLine("Invalid Account Type.");
                return;
            }

            var customer = new Customer
            {
                CustomerId = nextCustomerId++,
                FullName = name,
                Email = email,
                PhoneNumber = phone,
                CreatedAt = DateTime.Now
            };
            customers.Add(customer);

            string accountNumber = GenerateAccountNumber();

            try
            {
                var account = new BankAccount(accountNumber, customer, initialBalance, accountType);
                accounts.Add(account);
                Console.WriteLine($"Account Created Successfully. Account Number: {accountNumber}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private string GenerateAccountNumber()
        {
            string number;
            do
            {
                number = nextAccountNumber++.ToString();
            }
            while (accounts.Any(a => a.AccountNumber == number));

            return number;
        }

        public void Deposit()
        {
            Console.WriteLine("Enter Account Number ");
            string accNumber = Console.ReadLine();
            var account = accounts.FirstOrDefault(a => a.AccountNumber == accNumber);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.WriteLine("Enter Amount ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid Amount.");
                return;
            }

            try
            {
                account.Deposit(amount);
                Console.WriteLine($"Deposit Successful. New Balance: {account.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void Withdraw()
        {
            Console.WriteLine("Enter Account Number ");
            string accNumber = Console.ReadLine();
            var account = accounts.FirstOrDefault(a => a.AccountNumber == accNumber);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.WriteLine("Enter Amount ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid Amount.");
                return;
            }

            try
            {
                account.Withdraw(amount);
                Console.WriteLine($"Withdraw Successful. New Balance: {account.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void TransferMoney()
        {
            Console.WriteLine("Enter Source Account Number ");
            string sourceNumber = Console.ReadLine();
            var sourceAccount = accounts.FirstOrDefault(a => a.AccountNumber == sourceNumber);

            if (sourceAccount == null)
            {
                Console.WriteLine("Source account not found.");
                return;
            }

            Console.WriteLine("Enter Destination Account Number ");
            string destNumber = Console.ReadLine();
            var destAccount = accounts.FirstOrDefault(a => a.AccountNumber == destNumber);

            if (destAccount == null)
            {
                Console.WriteLine("Destination account not found.");
                return;
            }

            if (sourceNumber == destNumber)
            {
                Console.WriteLine("Source and destination accounts cannot be the same.");
                return;
            }

            Console.WriteLine("Enter Amount ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid Amount.");
                return;
            }

            try
            {
                sourceAccount.Withdraw(amount);
                destAccount.Deposit(amount);
                Console.WriteLine("Transfer Successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ViewAccDetails()
        {
            Console.WriteLine("Enter Account Number ");
            string accNumber = Console.ReadLine();
            var account = accounts.FirstOrDefault(a => a.AccountNumber == accNumber);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Account Number : {account.AccountNumber}");
            Console.WriteLine($"Customer Name  : {account.Customer.FullName}");
            Console.WriteLine($"Email          : {account.Customer.Email}");
            Console.WriteLine($"Phone          : {account.Customer.PhoneNumber}");
            Console.WriteLine($"Account Type   : {account.AccountType}");
            Console.WriteLine($"Balance        : {account.Balance}");
            Console.WriteLine($"Created At     : {account.CreatedAt}");
            Console.WriteLine($"Status         : {(account.IsActive ? "Active" : "Inactive")}");
            Console.WriteLine("---------------------------------");
        }

        public void ViewTransactionHistory()
        {
            Console.WriteLine("Enter Account Number ");
            string accNumber = Console.ReadLine();
            var account = accounts.FirstOrDefault(a => a.AccountNumber == accNumber);

            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            if (account.Transactions.Count == 0)
            {
                Console.WriteLine("No transactions yet for this account.");
                return;
            }

            var sortedTransactions = account.Transactions
                .OrderByDescending(t => t.TransactionDate)
                .ToList();

            Console.WriteLine("---------------------------------");
            foreach (var t in sortedTransactions)
            {
                Console.WriteLine($"{t.TransactionType} | Amount: {t.Amount} | Date: {t.TransactionDate} | " +
                                   $"Desc: {t.Description} | Balance After: {t.BalanceAfterTransaction}");
            }
            Console.WriteLine("---------------------------------");
        }

        public void ViewAllAccounts()
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts created yet.");
                return;
            }

            Console.WriteLine("---------------------------------");
            foreach (var account in accounts)
            {
                Console.WriteLine($"{account.AccountNumber} | {account.Customer.FullName} | " +
                                   $"{account.AccountType} | Balance: {account.Balance} | " +
                                   $"{(account.IsActive ? "Active" : "Inactive")}");
            }
            Console.WriteLine("---------------------------------");
        }
    }
}  