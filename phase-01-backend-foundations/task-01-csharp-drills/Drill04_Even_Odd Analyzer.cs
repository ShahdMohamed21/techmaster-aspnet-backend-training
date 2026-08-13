using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill04_Even_Odd_Analyzer
    {
        public static void Run()
        {

            Console.WriteLine("====Drill 04 — Even / Odd Analyzer====");
            Console.WriteLine("Enter The Number Of Numbers : ");
            int count;
            bool isCountValid = int.TryParse(Console.ReadLine(), out count);
            if (!isCountValid || count <= 0)
            {
                Console.WriteLine("The Count Must Be Greater Than 0");
            }
            else
            {


                List<int> odd = new List<int>();
                List<int> even = new List<int>();
                for (int i = 0; i < count; i++)
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
                    if (num % 2 == 0)
                    {
                        even.Add(num);
                    }
                    else
                    {
                        odd.Add(num);
                    }

                }
                Console.Write("Even | ");
                foreach (var i in even)
                {
                    Console.Write($" {i} ");
                }
                Console.Write("Odd | ");
                foreach (var i in odd)
                {
                    Console.Write($" {i} ");
                }
                Console.WriteLine();
                Console.WriteLine($"Even Count: {even.Count}");
                Console.WriteLine($"Odd Count: {odd.Count}");
            }

        }
        }
    }
