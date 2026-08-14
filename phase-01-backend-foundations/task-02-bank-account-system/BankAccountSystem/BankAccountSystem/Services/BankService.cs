using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task02.BankAccountSystem.Models;

namespace Task02.BankAccountSystem.Services
{
    public class BankService
    {
        private List<BankAccount> accounts = new List<BankAccount>();
        private List<Customer> customers = new List<Customer>();
        int NextId = 1;
        public void CreateAccount()
        {
            Console.WriteLine("Enter your Full Name ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your Email ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your Phone Number ");
            string Phone = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name) ||
               string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(Phone))
            {
                Console.WriteLine("Invalid Data");
                return;
            }
            customers.Add(new Customer
            {
                CustomerId = NextId++,
                FullName = name,
                Email = email,
                PhoneNumber = Phone,
                CreatedAt = DateTime.Now
            });



        }
        public void Deposit()
        {

        }
        public void Withdraw()
        {

        }
        public void TransferMoney()
        {

        }
        public void ViewAccDetails()
        {

        }

        public void ViewTransactionHistory()
        {

        }

        public void ViewAllAccounts()
        {

        }






    }
}


