using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02.BankAccountSystem.Models
{
    public class TransactionType
    {
        public enum TransactionTypes
        {
            Deposit,
            Withdraw,
            TransferOut,
            TransferIn
        }
    }
}
