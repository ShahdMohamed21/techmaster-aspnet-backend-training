using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill12_EmailValidator
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 12 — Email Validator ====");

            Console.Write("Enter Your Email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email Cannot Be Empty");
                return;
            }

            if (email.Contains(" "))
            {
                Console.WriteLine("Email Cannot Contain Spaces");
                return;
            }

            if (!email.Contains("@"))
            {
                Console.WriteLine("Email Must Contain @");
                return;
            }

            if (!email.Contains("."))
            {
                Console.WriteLine("Email Must Contain Dot");
                return;
            }

            if (email.StartsWith("@") || email.EndsWith("@"))
            {
                Console.WriteLine("Invalid Email");
                return;
            }

            Console.WriteLine("Valid Email");
        }
    }
}

