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
                // TryParse prevents the program from crashing when the user enters invalid input
                bool IsConverted = double.TryParse(Console.ReadLine(), out temp);
                if (IsConverted)
                {
                    // Convert Celsius to Fahrenheit using the formula: (C × 9 / 5) + 32
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
