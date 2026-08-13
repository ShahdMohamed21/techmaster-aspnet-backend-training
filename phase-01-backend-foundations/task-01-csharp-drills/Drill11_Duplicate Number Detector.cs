using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill11_Duplicate_Number_Detector
    {
        public static void Run()
        {
            // HashSet stores only unique numbers, so Add() returns false when a duplicate is found
            HashSet<int> numbers = new HashSet<int>();
            Console.Write("Enter Number Of Numbers: ");
            int count;
            bool isconvert=int.TryParse(Console.ReadLine(), out count);
            if(!isconvert || count <= 0)
            {
                Console.WriteLine("Invaild Input");
            }
           HashSet<int> Repeated=new HashSet<int>();
            for(int i=0; i<count; i++)
            {
                Console.WriteLine($"Enter Number {i + 1}");
                int num;
                bool isconverted = int.TryParse(Console.ReadLine(), out num);
                if (!isconverted )
                {
                    Console.WriteLine("Invaild Number");
                    i--;
                    continue;
                }
                if(!numbers.Add(num))
                {
                    // Store duplicates in a separate HashSet to avoid printing the same duplicate more than once

                    Repeated.Add(num);
                }

            }
            if(Repeated.Count==0)
            {
                Console.WriteLine("No duplicates found\r\n");
            }
            else
            {
                Console.WriteLine($"Duplicates:");
                foreach (var i in Repeated)
                {
                    Console.WriteLine($"{i}");
                }
                
            }
        }
    }
}
