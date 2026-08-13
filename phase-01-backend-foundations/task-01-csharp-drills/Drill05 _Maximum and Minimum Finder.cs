using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill05__Maximum_and_Minimum_Finder
    {
        public static void Run()
        {
            Console.WriteLine("====Drill 05 - Maximum and Minimum Finder====");
            Console.WriteLine("Enter The Number Of Numbers : ");
            int size;
            bool isSizeValid = int.TryParse(Console.ReadLine(), out size);
            if (!isSizeValid || size <= 0)
            {
                Console.WriteLine("The Count Must Be Greater Than 0");
            }
            else
            {
                List<int> numbers = new List<int>();
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine($"Enter Number{i + 1}");
                    int num;
                    bool isNumberValid = int.TryParse(Console.ReadLine(), out num);
                    if (!isNumberValid)
                    {
                        Console.WriteLine("Invalid Number");
                        i--;
                        continue;
                    }
                    numbers.Add(num);
                }
                int max = numbers[0];
                int min = numbers[0];
                foreach (int i in numbers)
                {
                    if (i > max)
                    {
                        max = i;
                    }
                    else if (i < min)
                    {
                        min = i;
                    }

                }
                Console.WriteLine($"Max {max} || Min {min}");
            }
        

        }
    }
}
