using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill13_PalindromeChecker
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 13 — Palindrome Checker ====");

            Console.Write("Enter Text: ");
            string text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Text Cannot Be Empty");
                return;
            }

            text = text.Trim().ToLower();
            text = text.Replace(" ", "");

            string reversed = "";

            for (int i = text.Length - 1; i >= 0; i--)
            {
                reversed += text[i];
            }

            if (text == reversed)
            {
                Console.WriteLine("Palindrome");
            }
            else
            {
                Console.WriteLine("Not Palindrome");
            }
        }
    }
}

