using System;
using System.Collections.Generic;
using Task02.BankAccountSystem.Models;

namespace Task02.BankAccountSystem.Models
{
    public class BankAccount
    {
        public string AccountNumber { get; private set; }
        public Customer Customer { get; private set; }
        public decimal Balance { get; private set; }
        public AccountType AccountType { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public BankAccount(string accountNumber, Customer customer, decimal initialBalance, AccountType accountType)
        {
            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative.");
            }

            AccountNumber = accountNumber;
            Customer = customer;
            Balance = initialBalance;
            AccountType = accountType;
            CreatedAt = DateTime.Now;
            IsActive = true;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }

            Balance += amount;

            Transactions.Add(new Transaction
            {
                AccountNumber = AccountNumber,
                TransactionType = TransactionType.Deposit,
                Amount = amount,
                TransactionDate = DateTime.Now,
                Description = "Deposit",
                BalanceAfterTransaction = Balance
            });
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdraw amount must be positive.");
            }
            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient balance.");
            }

            Balance -= amount;

            Transactions.Add(new Transaction
            {
                AccountNumber = AccountNumber,
                TransactionType = TransactionType.Withdraw,
                Amount = amount,
                TransactionDate = DateTime.Now,
                Description = "Withdraw",
                BalanceAfterTransaction = Balance
            });
        }
    }
}