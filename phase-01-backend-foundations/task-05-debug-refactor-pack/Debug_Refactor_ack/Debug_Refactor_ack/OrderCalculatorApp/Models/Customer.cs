using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug_Refactor_ack.OrderCalculatorApp.Models
{
    public class Customer
    {
        public string Name { get; set; } = string.Empty;

        public CustomerType Type { get; set; }
    }
}
