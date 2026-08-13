using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill14_SimpleExpenseTracker
    {

        public static void Run()
        {
            Console.WriteLine("==== Drill 14 — Simple Expense Tracker ====");

            Console.Write("Enter Number Of Expenses: ");

            int count;

            if (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.WriteLine("Invalid Count");
                return;
            }

            List<Expense> expenses = new List<Expense>();

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"Expense {i + 1}");

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Amount: ");

                decimal amount;

                if (!decimal.TryParse(Console.ReadLine(), out amount)|| amount <= 0)
                {
                    Console.WriteLine("Invalid Amount");
                    i--;
                    continue;
                }
                expenses.Add(new Expense
                {
                    Name = name,
                    Amount = amount
                });
            }

            decimal total = 0;
            decimal highestAmount = expenses[0].Amount;
            string highestName = expenses[0].Name;

            foreach (Expense expense in expenses)
            {
                total += expense.Amount;

                if (expense.Amount > highestAmount)
                {
                    highestAmount = expense.Amount;
                    highestName = expense.Name;
                }
            }

            decimal average = total / expenses.Count;

            Console.WriteLine();
            Console.WriteLine($"Total: {total:F2}");
            Console.WriteLine($"Average: {average:F2}");
            Console.WriteLine(
                $"Highest Expense: {highestName} - {highestAmount:F2}"
            );
        }
    }
}

