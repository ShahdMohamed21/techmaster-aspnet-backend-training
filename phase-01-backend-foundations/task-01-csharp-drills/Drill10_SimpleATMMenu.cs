using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill10_SimpleATMMenu
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 10 — Simple ATM Menu ====");

            // Decimal is used for money values to avoid floating-point precision issues

            decimal balance = 1000;
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Exit");

                Console.Write("Choose an option: ");
                int option;

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid Option");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine($"Balance: {balance:F2}");
                        break;

                    case 2:
                        Console.Write("Enter Deposit Amount: ");
                        decimal deposit;

                        if (!decimal.TryParse(Console.ReadLine(), out deposit))
                        {
                            Console.WriteLine("Invalid Amount");
                            break;
                        }

                        if (deposit <= 0)
                        {
                            Console.WriteLine("Deposit Must Be Positive");
                            break;
                        }

                        balance += deposit;

                        Console.WriteLine($"Deposit Successful");
                        Console.WriteLine($"New Balance: {balance:F2}");
                        break;

                    case 3:
                        Console.Write("Enter Withdraw Amount: ");

                        decimal withdraw;

                        if (!decimal.TryParse(Console.ReadLine(), out withdraw))
                        {
                            Console.WriteLine("Invalid Amount");
                            break;
                        }
                        // Prevent negative withdrawals and withdrawals that exceed the available balance

                        if (withdraw <= 0)
                        {
                            Console.WriteLine("Withdraw Amount Must Be Positive");
                            break;
                        }

                        if (withdraw > balance)
                        {
                            Console.WriteLine("Insufficient Balance");
                            break;
                        }

                        balance -= withdraw;

                        Console.WriteLine("Withdraw Successful");
                        Console.WriteLine($"New Balance: {balance:F2}");
                        break;

                    case 4:
                        isRunning = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }
    }
}

