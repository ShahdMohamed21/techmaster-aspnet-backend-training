


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
            bool IsConverted = double.TryParse(Console.ReadLine(), out temp);
            if (IsConverted)
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
            // Drill 04 — Even / Odd Analyzer
            Console.WriteLine("====Drill 04 — Even / Odd Analyzer====");
            Console.WriteLine("Enter The Number Of Numbers : ");
            int count;
            bool isCountValid = int.TryParse(Console.ReadLine(), out count);
            if (!isCountValid || count <= 0)
            {
                Console.WriteLine("The Count Must Be Greater Than 0");
            }
            else
            {


                List<int> odd = new List<int>();
                List<int> even = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"Enter Number{i + 1}");
                    int num;
                    bool isNumberValid = int.TryParse(Console.ReadLine(), out num);
                    if (!isNumberValid)
                    {
                        Console.WriteLine("Invalid Number");
                        i--;
                        continue;
                    }
                    if (num % 2 == 0)
                    {
                        even.Add(num);
                    }
                    else
                    {
                        odd.Add(num);
                    }

                }
                Console.Write("Even | ");
                foreach (var i in even)
                {
                    Console.Write($" {i} ");
                }
                Console.Write("Odd | ");
                foreach (var i in odd)
                {
                    Console.Write($" {i} ");
                }
                Console.WriteLine();
                Console.WriteLine($"Even Count: {even.Count}");
                Console.WriteLine($"Odd Count: {odd.Count}");
            }
           

            //Drill 05 - Maximum and Minimum Finder
            Console.WriteLine("====Drill 05 - Maximum and Minimum Finder====");
            Console.WriteLine("Enter The Number Of Numbers : ");
            int size;
            bool isSizeValid = int.TryParse(Console.ReadLine(), out size);
            if (!isSizeValid || size <= 0)
            {
                Console.WriteLine("The Count Must Be Greater Than 0");
            }
            else
            {
                List<int> numbers = new List<int>();
                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine($"Enter Number{i + 1}");
                    int num;
                    bool isNumberValid = int.TryParse(Console.ReadLine(), out num);
                    if (!isNumberValid)
                    {
                        Console.WriteLine("Invalid Number");
                        i--;
                        continue;
                    }
                    numbers.Add(num);
                }
                int max = numbers[0];
                int min = numbers[0];
                foreach (int i in numbers)
                {
                    if (i > max)
                    {
                        max = i;
                    }
                    else if (i < min)
                    {
                        min = i;
                    }
                   
                }
                Console.WriteLine($"Max {max} || Min {min}");
            }



            // Drill 06 — Word Counter
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
