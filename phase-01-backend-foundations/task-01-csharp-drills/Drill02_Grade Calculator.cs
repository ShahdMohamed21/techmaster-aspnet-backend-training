using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill02_Grade_Calculator
    {
        public static void Run()
        {
            

            Console.WriteLine("====Drill 02 — Grade Calculator====");
            Console.WriteLine("Enter Your Grade : ");
            int grade;
            bool isconverted = int.TryParse(Console.ReadLine(), out grade);
            if (isconverted)
            {
                if (grade < 0 || grade > 100)
                {
                    Console.WriteLine("The Grade Must Be Between 0 - 100");
                }
                else
                {
                    if (grade >= 90)
                    {
                        Console.WriteLine("A");
                    }
                    else if (grade >= 80 && grade < 90)
                    {
                        Console.WriteLine("B");
                    }
                    else if (grade >= 70 && grade < 80)
                    {
                        Console.WriteLine("C");
                    }
                    else if (grade >= 60 && grade < 70)
                    {
                        Console.WriteLine("D");
                    }
                    else
                    {
                        Console.WriteLine("F");
                    }
                }



            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
        }
    }
}
