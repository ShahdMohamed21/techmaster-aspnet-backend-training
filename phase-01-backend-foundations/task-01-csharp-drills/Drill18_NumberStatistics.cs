using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill18_NumberStatistics
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 18 — Number Statistics ====");

            Console.Write("Enter Number Of Numbers: ");

            int count;

            if (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                Console.WriteLine("Invalid Count");
                return;
            }

            List<int> numbers = new List<int>();

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

            int sum = 0;
            int positiveCount = 0;
            int negativeCount = 0;
            int zeroCount = 0;

            int max = numbers[0];
            int min = numbers[0];

            foreach (int number in numbers)
            {
                sum += number;

                if (number > 0)
                {
                    positiveCount++;
                }
                else if (number < 0)
                {
                    negativeCount++;
                }
                else
                {
                    zeroCount++;
                }

                if (number > max)
                {
                    max = number;
                }

                if (number < min)
                {
                    min = number;
                }
            }

            double average = (double)sum / numbers.Count;

            Console.WriteLine($"Count: {numbers.Count}");
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Average: {average:F2}");
            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Min: {min}");
            Console.WriteLine($"Positive Count: {positiveCount}");
            Console.WriteLine($"Negative Count: {negativeCount}");
            Console.WriteLine($"Zero Count: {zeroCount}");
        }
    }
}
