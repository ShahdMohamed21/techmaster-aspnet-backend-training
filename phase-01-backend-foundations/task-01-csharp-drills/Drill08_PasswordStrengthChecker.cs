using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill08_PasswordStrengthChecker
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 08 — Password Strength Checker ====");

            Console.WriteLine("Enter Your Password : ");
            string password = Console.ReadLine();
            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;
            bool hasSpecial = false;

            // Check each character to determine which password rules are satisfied
            if (!string.IsNullOrEmpty(password))
            {
                foreach (char ch in password)
                {
                    if (char.IsUpper(ch))
                    {
                        hasUpper = true;
                    }
                    else if (char.IsLower(ch))
                    {
                        hasLower = true;
                    }
                    else if (char.IsDigit(ch))
                    {
                        hasDigit = true;
                    }
                    else
                    {
                        hasSpecial = true;
                    }
                }
            }
            List<string> missingRules = new List<string>();
            // Store every missing requirement so the user knows exactly how to improve the password
            if (password.Length < 8)
            {
                missingRules.Add("at least 8 characters");
            }

            if (!hasUpper)
            {
                missingRules.Add("uppercase letter");
            }

            if (!hasLower)
            {
                missingRules.Add("lowercase letter");
            }

            if (!hasDigit)
            {
                missingRules.Add("digit");
            }
            if (!hasSpecial)
            {
                missingRules.Add("special character");
            }

            if (missingRules.Count == 0)
            {
                Console.WriteLine("Strong Password");
            }
            else
            {
                Console.WriteLine("Weak Password");
                Console.WriteLine("Missing:");

                foreach (string rule in missingRules)
                {
                    Console.WriteLine($"-{rule}-");
                }
            }
        }
    }
}

