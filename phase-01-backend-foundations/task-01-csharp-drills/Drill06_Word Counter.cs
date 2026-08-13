using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill06_Word_Counter
    {
        public static void Run()
        {
            Console.WriteLine("====Drill 06 — Word Counter====");

            Console.WriteLine("Enter A Sentence : ");
            string sentence = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(sentence))
            {
                Console.WriteLine("The Sentence Cannot Be Empty");
            }
            else
            {
                sentence = sentence.Trim();

                string[] words = sentence.Split(' ',
                    StringSplitOptions.RemoveEmptyEntries
                );

                Console.WriteLine($"Word Count: {words.Length}");
            }
        }

    }
}
