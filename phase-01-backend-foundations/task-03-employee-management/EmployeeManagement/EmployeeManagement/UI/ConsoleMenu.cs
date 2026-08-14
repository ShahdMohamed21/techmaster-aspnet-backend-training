
using EmployeeManagement.Models;
using EmployeeManagement.Services;

namespace EmployeeManagement.UI
{
    public class ConsoleMenu
    {
        private EmployeeService employeeService = new EmployeeService();
        private EmployeeReportService reportService;
        public void Run()
        {
            reportService = new EmployeeReportService(employeeService);

            bool IsRunning = false;

            while (!IsRunning)
            {
               

                Console.WriteLine("======================================");
                Console.WriteLine("     Employee Management System");
                Console.WriteLine("======================================");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Update Employee");
                Console.WriteLine("3. Deactivate Employee");
                Console.WriteLine("4. Search Employee");
                Console.WriteLine("5. Filter by Department");
                Console.WriteLine("6. Sort Employees");
                Console.WriteLine("7. Show Salary Reports");
                Console.WriteLine("8. View All Employees");
                Console.WriteLine("9. Exit");
                Console.WriteLine("======================================");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            employeeService.AddEmployee();
                            break;

                        case "2":
                            Console.Write("Enter Employee ID: ");
                            string Id = Console.ReadLine();
                            employeeService.UpdateEmployee(Id);
                            break;

                        case "3":
                            Console.Write("Enter Employee ID: ");
                            string deactivateId = Console.ReadLine();
                            employeeService.Deactivate_Employee(deactivateId);
                            break;

                        case "4":
                            SearchEmployee();
                            break;
                        case "5":
                            FilterByDepartment();
                            break;

                        case "6":
                            SortEmployees();
                            break;

                        case "7":
                            reportService.ShowSalaryReports();
                            break;

                        case "8":
                            ViewAllEmployees();
                            break;

                        case "9":
                            IsRunning = true;
                            Console.WriteLine("Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void SearchEmployee()
        {

            Console.WriteLine("========== Search Employee ==========");
            Console.WriteLine("1. Search by ID");
            Console.WriteLine("2. Search by Name");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            Console.Write("Enter search value: ");
            string value = Console.ReadLine();

            if (choice == "1")
            {
                employeeService.SearchById(value);
            }
            else if (choice == "2")
            {
                employeeService.SearchByName(value);
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }

        }

        private void FilterByDepartment()
        {
            Console.WriteLine("========== Filter By Department ==========");
            Console.Write("Enter Department: ");

            string departmentInput = Console.ReadLine();

            if (!Enum.TryParse<Department>(
                departmentInput,
                true,
                out Department department))
            {
                Console.WriteLine("Invalid Department.");
                return;
            }

            employeeService.Filter_by_Department(department);
        }

        private void SortEmployees()
        {
           

            Console.WriteLine("========== Sort Employees ==========");
            Console.WriteLine("1. Salary Ascending");
            Console.WriteLine("2. Salary Descending");
            Console.WriteLine("3. Hire Date Ascending");
            Console.WriteLine("4. Hire Date Descending");
            Console.WriteLine("5. Name");

            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    employeeService.SortBySalaryAscending();
                    break;

                case "2":
                    employeeService.SortBySalaryDescending();
                    break;

                case "3":
                    employeeService.SortByHireDateAscending();
                    break;

                case "4":
                    employeeService.SortByHireDateDescending();
                    break;

                case "5":
                    employeeService.SortByName();
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }


        }

        private void ViewAllEmployees()
        {

            Console.WriteLine("========== All Employees ==========");

            employeeService.ViewAllEmployees();

        }

    }
}
