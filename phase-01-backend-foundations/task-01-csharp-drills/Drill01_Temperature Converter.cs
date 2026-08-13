using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill01_Temperature_Converter
    {
        public static void Run()
        {
            {
                Console.WriteLine("====Drill 01 — Temperature Converter====");
                Console.WriteLine("Enter The Temperature : ");
                double temp;
                bool IsConverted = double.TryParse(Console.ReadLine(), out temp);
                if (IsConverted)
                {
                    Console.WriteLine($"Fahrenheit : {temp * 9 / 5 + 32:F2} ");

                }
                else
                {
                    Console.WriteLine("Invalid Temp");
                }
            }
        }
    }
}
