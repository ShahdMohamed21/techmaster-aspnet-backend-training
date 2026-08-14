using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02.BankAccountSystem.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public string AccountNumber { get; set; }

        public TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string Description { get; set; }

        public decimal BalanceAfterTransaction { get; set; }
    }
}
