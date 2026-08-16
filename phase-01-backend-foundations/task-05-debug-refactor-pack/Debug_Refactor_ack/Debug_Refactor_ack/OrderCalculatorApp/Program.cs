using Debug_Refactor_ack.OrderCalculatorApp.Models;
using Debug_Refactor_ack.OrderCalculatorApp.Services;
using Task05.Services;

namespace Debug_Refactor_ack.OrderCalculatorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleMenu menu = new ConsoleMenu();
            OrderCalculator calculator = new OrderCalculator();
            ReceiptPrinter printer = new ReceiptPrinter();

            Customer customer = menu.GetCustomer();
            Order order = menu.GetOrder();

            decimal subtotal = calculator.CalculateSubtotal(order);

            decimal discount = calculator.CalculateDiscount(order, customer);

            decimal amountAfterDiscount = subtotal - discount;

            decimal tax = calculator.CalculateTax(amountAfterDiscount);

            decimal shipping = calculator.CalculateShipping(amountAfterDiscount);

            decimal finalTotal = calculator.CalculateFinalTotal(order,customer);

            printer.PrintReceipt(
                customer,
                order,
                subtotal,
                discount,
                tax,
                shipping,
                finalTotal);
        }
    }
}

