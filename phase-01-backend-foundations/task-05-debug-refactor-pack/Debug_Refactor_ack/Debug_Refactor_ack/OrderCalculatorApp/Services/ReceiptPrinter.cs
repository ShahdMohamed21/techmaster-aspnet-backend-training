using Debug_Refactor_ack.OrderCalculatorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug_Refactor_ack.OrderCalculatorApp.Services
{
    public class ReceiptPrinter
    {
        public void PrintReceipt(Customer customer, Order order,decimal subtotal,
            decimal discount,decimal tax, decimal shipping, decimal finalTotal)
        {
            Console.WriteLine();
            Console.WriteLine("========== ORDER RECEIPT ==========");
            Console.WriteLine($"Customer: {customer.Name}");
            Console.WriteLine($"Customer Type: {customer.Type}");
            Console.WriteLine($"Product: {order.ProductName}");
            Console.WriteLine($"Price: {order.Price:F2}");
            Console.WriteLine($"Quantity: {order.Quantity}");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Subtotal: {subtotal:F2}");
            Console.WriteLine($"Discount: {discount:F2}");
            Console.WriteLine($"Tax: {tax:F2}");
            Console.WriteLine($"Shipping: {shipping:F2}");
            Console.WriteLine($"Final Total: {finalTotal:F2}");
            Console.WriteLine("===================================");
        }
    }
}
