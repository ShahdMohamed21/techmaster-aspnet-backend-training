using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class EmployeeReportService
    {
        private EmployeeService employeeService;
        public EmployeeReportService(EmployeeService service)
        {
            employeeService = service;
        }
        public void ShowSalaryReports()
        {
            var employees = employeeService.GetEmployees();

            if (employees.Count == 0)
            {
                Console.WriteLine("No Employees Found");
                return;
            }
            var averageSalary = employees.Average(x => x.Salary);

            var highestSalary = employees
                .OrderByDescending(x => x.Salary)
                .First();

            var lowestSalary = employees
                .OrderBy(x => x.Salary)
                .First();

            var totalPayroll = employees.Sum(x => x.Salary);

            Console.WriteLine("===== Salary Reports =====");
            Console.WriteLine($"Average Salary : {averageSalary}");
            Console.WriteLine($"Highest Salary : {highestSalary.FullName} - {highestSalary.Salary}");
            Console.WriteLine($"Lowest Salary : {lowestSalary.FullName} - {lowestSalary.Salary}");
            Console.WriteLine($"Total Payroll : {totalPayroll}");

            Console.WriteLine();
            Console.WriteLine("Employees Count By Department:");

            var departmentCounts = employees.GroupBy(x => x.Department);

            foreach (var group in departmentCounts)
            {
                Console.WriteLine($"{group.Key} : {group.Count()}");
            }

            var activeCount = employees.Count(x => x.IsActive);
            var inactiveCount = employees.Count(x => !x.IsActive);

            Console.WriteLine();
            Console.WriteLine($"Active Employees : {activeCount}");
            Console.WriteLine($"Inactive Employees : {inactiveCount}");
        }
    }
}