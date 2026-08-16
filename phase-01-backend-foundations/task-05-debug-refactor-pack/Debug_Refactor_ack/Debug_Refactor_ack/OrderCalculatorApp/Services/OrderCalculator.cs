using Debug_Refactor_ack.OrderCalculatorApp.Models;


namespace Task05.Services
{
    public class OrderCalculator
    {
        private const decimal TaxRate = 0.14m;
        private const decimal ShippingCost = 50m;
        private const decimal FreeShippingLimit = 1000m;

        public decimal CalculateSubtotal(Order order)
        {
            return order.Price * order.Quantity;
        }

        public decimal CalculateDiscount(Order order, Customer customer)
        {
            decimal subtotal = CalculateSubtotal(order);

            if (customer.Type == CustomerType.Regular)
            {
                return 0;
            }
            else if (customer.Type == CustomerType.Silver)
            {
                return subtotal * 0.05m;
            }
            else if (customer.Type == CustomerType.Gold)
            {
                return subtotal * 0.10m;
            }
            else if (customer.Type == CustomerType.VIP)
            {
                return subtotal * 0.15m;
            }
            else
            {
                return 0;
            }
        }

        public decimal CalculateTax(decimal amountAfterDiscount)
        {
            return amountAfterDiscount * TaxRate;
        }

        public decimal CalculateShipping(decimal amountAfterDiscount)
        {
            if (amountAfterDiscount >= FreeShippingLimit)
            {
                return 0;
            }
            else
            {
                return ShippingCost;
            }
        }

        public decimal CalculateFinalTotal(Order order, Customer customer)
        {
            decimal subtotal = CalculateSubtotal(order);
            decimal discount = CalculateDiscount(order, customer);
            decimal amountAfterDiscount = subtotal - discount;
            decimal tax = CalculateTax(amountAfterDiscount);
            decimal shipping = CalculateShipping(amountAfterDiscount);

            return amountAfterDiscount + tax + shipping;
        }
    }
}