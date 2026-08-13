using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    internal class Drill03_Simple_Login_Validator
    {
        public static void Run()
        { 
            Console.WriteLine("====Drill 03 — Simple Login Validator====");
            string UserName = "ShahdMohamed";
            string Password = "Shahd#1234";
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine("Enter Your UserName : ");
                string user = Console.ReadLine();

                Console.WriteLine("Enter Your Password : ");
                string pass = Console.ReadLine();

                bool isUsernameCorrect =
                    string.Equals(user, UserName, StringComparison.OrdinalIgnoreCase);

                bool isPasswordCorrect = Password == pass;

                if (isUsernameCorrect && isPasswordCorrect)
                {
                    Console.WriteLine("Login Successfully");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Login");
                }

                if (i == 3)
                {
                    Console.WriteLine("Account Locked");
                }
            }
        }
    }
}
