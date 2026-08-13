using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill07_NameFormatter
    {

        public static void Run()
        {
            Console.WriteLine("==== Drill 07 — Name Formatter ====");

            Console.WriteLine("Enter Your Full Name : ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name Cannot Be Empty");
                return;
            }

            string[] parts = name.Split(' ',StringSplitOptions.RemoveEmptyEntries);
            List<string> formattedParts = new List<string>();

            foreach (string part in parts)
            {
                string lowerPart = part.ToLower();
                string formattedPart =
                    char.ToUpper(lowerPart[0]) + lowerPart.Substring(1);

                formattedParts.Add(formattedPart);
            }

            string formattedName = string.Join(" ", formattedParts);

            Console.WriteLine($"Formatted Name: {formattedName}");
        }
    }
}

