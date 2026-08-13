using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill17_SimpleSearchEngine
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 17 — Simple Search Engine ====");

            List<string> names = new List<string>
            {
                "Shahd Mohamed",
                "Abdallah Mohamed",
                "Layan Mohamed",
                "Hamaza Mahmoud",
                "Mariam Ali"
            };

            Console.Write("Enter Search Keyword: ");
            string keyword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                Console.WriteLine("Keyword Cannot Be Empty");
                return;
            }

            bool found = false;

            foreach (string name in names)
            {
                if (name.Contains(keyword,StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(name);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No Results Found");
            }
        }
    }
}
