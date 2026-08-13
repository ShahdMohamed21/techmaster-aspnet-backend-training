namespace Task01
{
    internal class Drill20
    {
        public static void Run()
        {
            Console.WriteLine("==== Drill 20 — Refactoring ====");

            Console.WriteLine("\n--- Grade Calculator ---");
            RunGradeCalculator();

            Console.WriteLine("\n--- ATM ---");
            RunATM();

            Console.WriteLine("\n--- Frequency Counter ---");
            RunFrequencyCounter();
        }

        static void RunGradeCalculator()
        {
            int grade = ReadGrade();

            if (!IsValidGrade(grade))
            {
                Console.WriteLine("The Grade Must Be Between 0 - 100");
                return;
            }

            char result = CalculateGrade(grade);

            PrintGrade(result);
        }

        static int ReadGrade()
        {
            Console.Write("Enter Your Grade: ");

            int grade;

            while (!int.TryParse(Console.ReadLine(), out grade))
            {
                Console.WriteLine("Invalid Input");
                Console.Write("Enter Your Grade: ");
            }

            return grade;
        }

     
        static bool IsValidGrade(int grade)
        {
            return grade >= 0 && grade <= 100;
        }

        static char CalculateGrade(int grade)
        {
            if (grade >= 90)
                return 'A';

            if (grade >= 80)
                return 'B';

            if (grade >= 70)
                return 'C';

            if (grade >= 60)
                return 'D';

            return 'F';
        }

        static void PrintGrade(char grade)
        {
            Console.WriteLine($"Your Grade: {grade}");
        }

        static void RunATM()
        {
            decimal balance = 1000;
            bool isRunning = true;

            while (isRunning)
            {
                PrintATMMenu();

                int option = ReadATMOption();

                switch (option)
                {
                    case 1:
                        PrintBalance(balance);
                        break;

                    case 2:
                        balance = Deposit(balance);
                        break;

                    case 3:
                        balance = Withdraw(balance);
                        break;

                    case 4:
                        isRunning = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }

        static void PrintATMMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
        }


        static int ReadATMOption()
        {
            int option;

            while (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid Option");
                Console.Write("Choose an option: ");
            }

            return option;
        }

        static void PrintBalance(decimal balance)
        {
            Console.WriteLine($"Balance: {balance:F2}");
        }

        static decimal Deposit(decimal balance)
        {
            Console.Write("Enter Deposit Amount: ");

            decimal amount;

            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid Amount");
                return balance;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Deposit Must Be Positive");
                return balance;
            }

            balance += amount;

            Console.WriteLine("Deposit Successful");
            Console.WriteLine($"New Balance: {balance:F2}");

            return balance;
        }

  
        static decimal Withdraw(decimal balance)
        {
            Console.Write("Enter Withdraw Amount: ");

            decimal amount;

            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid Amount");
                return balance;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Withdraw Amount Must Be Positive");
                return balance;
            }

            if (amount > balance)
            {
                Console.WriteLine("Insufficient Balance");
                return balance;
            }

            balance -= amount;

            Console.WriteLine("Withdraw Successful");
            Console.WriteLine($"New Balance: {balance:F2}");

            return balance;
        }
        static void RunFrequencyCounter()
        {
            List<int> numbers = ReadNumbers();

            Dictionary<int, int> frequency = CountFrequency(numbers);

            PrintFrequency(frequency);
        }

     
        static List<int> ReadNumbers()
        {
            List<int> numbers = new List<int>();

            Console.Write("Enter Number Of Numbers: ");

            int count;

            while (!int.TryParse(Console.ReadLine(), out count)
                   || count <= 0)
            {
                Console.WriteLine("Invalid Count");
                Console.Write("Enter Number Of Numbers: ");
            }

            for (int i = 0; i < count; i++)
            {
                Console.Write($"Enter Number {i + 1}: ");

                int number;

                if (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.WriteLine("Invalid Number");
                    i--;
                    continue;
                }

                numbers.Add(number);
            }

            return numbers;
        }

    
        static Dictionary<int, int> CountFrequency(List<int> numbers)
        {
            Dictionary<int, int> frequency =
                new Dictionary<int, int>();

            foreach (int number in numbers)
            {
                if (frequency.ContainsKey(number))
                {
                    frequency[number]++;
                }
                else
                {
                    frequency.Add(number, 1);
                }
            }

            return frequency;
        }

       
        static void PrintFrequency(Dictionary<int, int> frequency)
        {
            Console.WriteLine("Frequency:");

            foreach (var item in frequency)
            {
                Console.WriteLine($"{item.Key} => {item.Value}");
            }
        }
    }
}