using System;
using System.Collections.Generic;
using Task02.BankAccountSystem.Models;

namespace Task02.BankAccountSystem.Models
{
    public class BankAccount
    {
        public string AccountNumber { get; set; }

        public Customer Customer { get; set; }

        public decimal Balance { get; private set; }

        public AccountType AccountType { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public BankAccount(decimal initialBalance)
        {
            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative.");
            }

            Balance = initialBalance;
        }
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.");
            }

            Balance += amount;
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
        }
    }
}