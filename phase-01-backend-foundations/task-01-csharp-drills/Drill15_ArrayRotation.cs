using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill15_ArrayRotation
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 15 — Array Rotation ====");

            int[] numbers = { 1, 2, 3, 4, 5 };

            Console.WriteLine("Before Rotation:");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.WriteLine();

            int last = numbers[numbers.Length - 1];

            for (int i = numbers.Length - 1; i >= 1; i--)
            {
                numbers[i] = numbers[i - 1];
            }

            numbers[0] = last;

            Console.WriteLine("After Rotation:");

            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.WriteLine();
        }
    }
}
