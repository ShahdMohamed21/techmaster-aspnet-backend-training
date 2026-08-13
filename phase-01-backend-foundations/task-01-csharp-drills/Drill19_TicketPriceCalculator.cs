using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill19_TicketPriceCalculator
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 19 — Simple Ticket Price Calculator ====");

            decimal basePrice = 100;
            decimal discount = 0;

            Console.Write("Enter Your Age: ");

            int age;

            if (!int.TryParse(Console.ReadLine(), out age) || age < 0)
            {
                Console.WriteLine("Invalid Age");
                return;
            }

            Console.Write("Are You A Student? (yes/no): ");
            string studentInput = Console.ReadLine();

            bool isStudent =string.Equals( studentInput,"yes",StringComparison.OrdinalIgnoreCase);
            if (age < 12)
            {
                discount = Math.Max(discount, 0.50m);
            }

            if (age > 60)
            {
                discount = Math.Max(discount, 0.30m);
            }

            if (isStudent)
            {
                discount = Math.Max(discount, 0.20m);
            }

            decimal finalPrice = basePrice * (1 - discount);

            Console.WriteLine($"Base Price: {basePrice:F2}");
            Console.WriteLine($"Discount: {discount:P0}");
            Console.WriteLine($"Final Price: {finalPrice:F2}");
        }
    }
}
