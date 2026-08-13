
namespace Task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Drill 01 — Temperature Converter
            Console.WriteLine("====Drill 01 — Temperature Converter====");
            Console.WriteLine("Enter The Temperature : ");
            double temp;
            bool IsConverted = double.TryParse(Console.ReadLine() , out temp);
            if(IsConverted)
            {
                Console.WriteLine($"Fahrenheit : {temp * 9 / 5 + 32:F2} ");

            }
            else
            {
                Console.WriteLine("Invalid Temp");
            }

            //Drill 02 — Grade Calculator
            Console.WriteLine("====Drill 02 — Grade Calculator====");
            Console.WriteLine("Enter Your Grade : ");
            int grade;
            bool isconverted= int.TryParse(Console.ReadLine(),out grade);
            if(isconverted)
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

            //Drill 03 — Simple Login Validator
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
