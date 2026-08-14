using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task02.BankAccountSystem.Services;


namespace Task02.BankAccountSystem.UI
{
    public class ConsoleMenu
    {  
        public void Run()
        {
            BankService service = new BankService();
            bool IsRunning = true;
            while (IsRunning)
            {
                Console.WriteLine("====== TechMaster Bank System ======");
                Console.WriteLine("1.Create Customer Account");
                Console.WriteLine("2.Deposit Money");
                Console.WriteLine("3.Withdraw Money");
                Console.WriteLine("4.Transfer Money");
                Console.WriteLine("5.View Account Details");
                Console.WriteLine("6.View Transaction History");
                Console.WriteLine("7.View All Accounts");
                Console.WriteLine("8.Exit");
                Console.WriteLine("Choose an option:");
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid Option");
                    continue;
                }
                switch (option)
                {
                    case 1:
                        service.CreateAccount();
                        break;
                    case 2:
                        service.Deposit();
                        break;
                    case 3:
                        service.Withdraw();
                        break;
                    case 4:
                        service.TransferMoney();
                        break;
                    case 5:
                        service.ViewAccDetails();
                        break;
                    case 6:
                        service.ViewTransactionHistory();
                        break;
                    case 7:
                        service.ViewAllAccounts();
                        break;
                    case 8:
                        IsRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }


        }
    }
}
