using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill09_ShoppingCartTotal
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 09 — Shopping Cart Total ====");

            Console.WriteLine("Enter Number Of Items : ");

            int itemCount;
            bool isCountValid = int.TryParse(Console.ReadLine(),out itemCount);

            if (!isCountValid || itemCount <= 0)
            {
                Console.WriteLine("Invalid Number Of Items");
                return;
            }

            decimal total = 0;

            for (int i = 0; i < itemCount; i++)
            {
                Console.WriteLine($"Item {i + 1}");

                Console.Write("Enter Price : ");
                decimal price;
                bool isPriceValid = decimal.TryParse(Console.ReadLine(),out price
                );

                if (!isPriceValid || price <= 0)
                {
                    Console.WriteLine("Invalid Price");
                    i--;
                    continue;
                }

                Console.Write("Enter Quantity : ");
                int quantity;
                bool isQuantityValid = int.TryParse( Console.ReadLine(),out quantity);

                if (!isQuantityValid || quantity <= 0)
                {
                    Console.WriteLine("Invalid Quantity");
                    i--;
                    continue;
                }

                decimal subtotal = price * quantity;
                total += subtotal;
            }

            decimal discount = 0;

            if (total > 1000)
            {
                discount = total * 0.10m;
            }

            decimal finalTotal = total - discount;

            Console.WriteLine($"Subtotal: {total:F2}");
            Console.WriteLine($"Discount: {discount:F2}");
            Console.WriteLine($"Final Total: {finalTotal:F2}");
        }
    }
}

