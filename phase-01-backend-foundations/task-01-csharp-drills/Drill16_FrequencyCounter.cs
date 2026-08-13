using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill16_FrequencyCounter
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 16 — Frequency Counter ====");

            List<int> numbers = new List<int>();

            Console.Write("Enter Number Of Numbers: ");

            int count;

            if (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.WriteLine("Invalid Count");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.Write($"Enter Number {i + 1}: ");

                int number;

                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Invalid Number");
                    i--;
                    continue;
                }

                numbers.Add(number);
            }

            Dictionary<int, int> frequency = new Dictionary<int, int>();

            foreach (int number in numbers)
            {
                if (frequency.ContainsKey(number))
                {
                    frequency[number]++;
                }
                else
                {
                    frequency.Add(number, 1);
                }
            }

            Console.WriteLine("Frequency:");

            foreach (var item in frequency)
            {
                Console.WriteLine($"{item.Key} => {item.Value}");
            }
        }
    }
}
