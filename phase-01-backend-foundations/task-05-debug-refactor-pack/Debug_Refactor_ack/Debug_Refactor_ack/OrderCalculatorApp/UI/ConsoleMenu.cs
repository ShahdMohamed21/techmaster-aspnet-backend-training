using Debug_Refactor_ack.OrderCalculatorApp.Models;

namespace Task05.Services
{
    public class ConsoleMenu
    {
        public Customer GetCustomer()
        {
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Customer name cannot be empty. Enter customer name: ");
                name = Console.ReadLine();
            }

            CustomerType type = GetCustomerType();

            return new Customer
            {
                Name = name,
                Type = type
            };
        }

        public Order GetOrder()
        {
            Console.Write("Enter product name: ");
            string productName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(productName))
            {
                Console.Write("Product name cannot be empty. Enter product name: ");
                productName = Console.ReadLine();
            }

            Console.Write("Enter product price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            while (price <= 0)
            {
                Console.Write("Price must be positive. Enter product price: ");
                price = decimal.Parse(Console.ReadLine());
            }

            Console.Write("Enter quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            while (quantity <= 0)
            {
                Console.Write("Quantity must be positive. Enter quantity: ");
                quantity = int.Parse(Console.ReadLine());
            }

            return new Order
            {
                ProductName = productName,
                Price = price,
                Quantity = quantity
            };
        }

        private CustomerType GetCustomerType()
        {
            while (true)
            {
                Console.Write("Enter customer type (Regular/Silver/Gold/VIP): ");

                string input = Console.ReadLine();

                if (input.ToLower() == "regular")
                {
                    return CustomerType.Regular;
                }
                else if (input.ToLower() == "silver")
                {
                    return CustomerType.Silver;
                }
                else if (input.ToLower() == "gold")
                {
                    return CustomerType.Gold;
                }
                else if (input.ToLower() == "vip")
                {
                    return CustomerType.VIP;
                }

                Console.WriteLine("Invalid customer type. Please try again.");
            }
        }
    }
}