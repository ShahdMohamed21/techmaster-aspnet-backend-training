using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services
{

    public class EmployeeService
    {
        private List<Employee> employees = new List<Employee>();

        int NextId = 13;
        private Department dept;



        public EmployeeService()
        {
            employees.Add(new Employee
            {
                EmployeeId = "EMP-001",
                FullName = "Mohamed Ayman",
                Email = "mohamed@test.com",
                Department = Department.IT,
                Position = "Backend Developer",
                Salary = 20000,
                HireDate = new DateTime(2025, 1, 10),
                IsActive = true
            });
            employees.Add(new Employee
            {
                EmployeeId = "EMP-002" ,
                FullName = "Sara Adel",
                Email = "sara@test.com",
                Department = Department.HR,
                Position = "HR Specialist",
                Salary = 12000,
                HireDate = new DateTime(2024, 5, 15),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-003",
                FullName = "Ahmed Tarek",
                Email = "ahmed@test.com",
                Department = Department.IT,
                Position = "Junior Developer",
                Salary = 9000,
                HireDate = new DateTime(2026, 1, 1),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-004" ,
                FullName = "Omar Samir",
                Email = "omar@test.com",
                Department = Department.Sales,
                Position = "Sales Executive",
                Salary = 11000,
                HireDate = new DateTime(2023, 11, 20),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-005" ,
                FullName = "Mariam Hassan",
                Email = "mariam@test.com",
                Department = Department.Finance,
                Position = "Accountant",
                Salary = 14000,
                HireDate = new DateTime(2022, 9, 11),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-006" ,
                FullName = "Khaled Ali",
                Email = "khaled@test.com",
                Department = Department.IT,
                Position = "DevOps Trainee",
                Salary = 10000,
                HireDate = new DateTime(2026, 2, 1),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-007" ,
                FullName = "Nour Emad",
                Email = "nour@test.com",
                Department = Department.Marketing,
                Position = "Content Specialist",
                Salary = 9500,
                HireDate = new DateTime(2025, 7, 8),
                IsActive = true
            });
            employees.Add(new Employee
            {
                EmployeeId = "EMP-008" ,
                FullName = "Youssef Nabil",
                Email = "youssef@test.com",
                Department = Department.Sales,
                Position = "Sales Manager",
                Salary = 18000,
                HireDate = new DateTime(2021, 3, 17),
                IsActive = false
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-009" ,
                FullName = "Dina Farouk",
                Email = "dina@test.com",
                Department = Department.HR,
                Position = "Recruiter",
                Salary = 10500,
                HireDate = new DateTime(2024, 2, 13),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-010",
                FullName = "Hady Mahmoud",
                Email = "hady@test.com",
                Department = Department.IT,
                Position = "QA Engineer",
                Salary = 13000,
                HireDate = new DateTime(2025, 10, 1),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-011" ,
                FullName = "Salma Taha",
                Email = "salma@test.com",
                Department = Department.Finance,
                Position = "Finance Manager",
                Salary = 26000,
                HireDate = new DateTime(2020, 12, 12),
                IsActive = true
            });

            employees.Add(new Employee
            {
                EmployeeId = "EMP-012" ,
                FullName = "Ali Mostafa",
                Email = "ali@test.com",
                Department = Department.Support,
                Position = "Support Agent",
                Salary = 8000,
                HireDate = new DateTime(2026, 3, 5),
                IsActive = true
            });
        }
        public List<Employee> GetEmployees()
        {
            return employees;
        }


        public void AddEmployee()
        {

            Console.WriteLine("Please Enter Your Name : ");
            string name=Console.ReadLine();
            if(string.IsNullOrEmpty(name) )
            {
                throw new Exception("Name Is Required");
                
            }
            Console.WriteLine("Please Enter Your Email : ");
            string email = Console.ReadLine();
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Email Is Required");
                
            }
            Console.WriteLine("Please Choose Your Department : ");
            Console.WriteLine($"1.{Department.Finance}");
            Console.WriteLine($"2.{Department.Support}");
            Console.WriteLine($"3.{Department.HR}");
            Console.WriteLine($"4.{Department.Marketing}");
            Console.WriteLine($"5.{Department.IT}");
            Console.WriteLine($"6.{Department.Sales}");

            int option;
            if(!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invaild Department");
                return;
            }
            switch(option)
            {
                case 1:
                    dept=Department.Finance; break;
                case 2:
                    dept= Department.Support; break;
                case 3:
                    dept= Department.HR; break;
                case 4:
                    dept=Department.Marketing; break;
                case 5:
                    dept=Department.IT; break;
                case 6:
                    dept=Department.Sales; break;
                default:
                    Console.WriteLine("Invaild Department");

                    return;
            }
            Console.WriteLine("Please Enter Your Position : ");
            string pos = Console.ReadLine();
            if (string.IsNullOrEmpty(pos))
            {
                throw new Exception("Position is required");
            }
            Console.WriteLine("Please Enter Your Salary : ");
            int sal;
            if (!int.TryParse(Console.ReadLine(), out sal) || sal<=0)
            {
                Console.WriteLine("Invaild Salary");
                return;
            }
            Console.WriteLine("Please Enter Hire Date (yyyy-MM-dd):");

            if (!DateTime.TryParse(Console.ReadLine(), out DateTime hireDate))
            {
                Console.WriteLine("Invalid Hire Date");
                return;
            }
            if (hireDate > DateTime.Now)
            {
                Console.WriteLine("Hire Date cannot be in the future");
                return;
            }
            DateTime Created= DateTime.Now;
            Console.WriteLine("Please Enter Your Phone Number : ");
            string ph= Console.ReadLine();
            Console.WriteLine("Please Enter Your Manager Name: ");
            string man= Console.ReadLine();
            employees.Add(new Employee
            {
                EmployeeId = $"EMP-{NextId++:D3}",
                FullName = name,
                Email = email,
                Department = dept,
                Position = pos,
                Salary = sal,
                HireDate = hireDate,
                IsActive = true,
                PhoneNumber = ph,
                ManagerName = man,
                CreatedAt = Created,
            });
            Console.WriteLine("Employee Added Successfully!");




        }

        public void UpdateEmployee(string id)
        {
            var emp = employees.FirstOrDefault(x => x.EmployeeId == id);
            if (emp == null)
            {
                throw new Exception("No Employee Found!");

            }
            Console.WriteLine("Please Enter Your New Email : ");
            string email = Console.ReadLine();
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Email Is Required");

            }
            Console.WriteLine("Please Choose Your New Department : ");
            Console.WriteLine($"1.{Department.Finance}");
            Console.WriteLine($"2.{Department.Support}");
            Console.WriteLine($"3.{Department.HR}");
            Console.WriteLine($"4.{Department.Marketing}");
            Console.WriteLine($"5.{Department.IT}");
            Console.WriteLine($"6.{Department.Sales}");

            int option;
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invaild Department");
                return;
            }
            switch (option)
            {
                case 1:
                    dept = Department.Finance; break;
                case 2:
                    dept = Department.Support; break;
                case 3:
                    dept = Department.HR; break;
                case 4:
                    dept = Department.Marketing; break;
                case 5:
                    dept = Department.IT; break;
                case 6:
                    dept = Department.Sales; break;
                default:
                    Console.WriteLine("Invaild Department");

                    return;
            }
            Console.WriteLine("Please Enter Your New Position : ");
            string pos = Console.ReadLine();
            if (string.IsNullOrEmpty(pos))
            {
                throw new Exception("Position is required");
            }
            Console.WriteLine("Please Enter Your New Salary : ");
            int sal;
            if (!int.TryParse(Console.ReadLine(), out sal) || sal <= 0)
            {
                Console.WriteLine("Invaild Salary");
                return;
            }
            emp.Email = email;
            emp.Salary = sal;
            emp.Position = pos;
            emp.Department = dept;
            Console.WriteLine("Employee Updated Successfully!");
        

        }

        public void Deactivate_Employee(string id)
        {
            var emp = employees.FirstOrDefault(x => x.EmployeeId == id);

            if (emp == null)
            {
                throw new Exception("No Employee Found");
            }

            if (!emp.IsActive)
            {
                Console.WriteLine("Employee is already inactive");
                return;
            }

            emp.IsActive = false;

            Console.WriteLine("Employee Deactivated Successfully");
        }

        public void SearchById(string id)
        {
            var emp = employees.FirstOrDefault(x => x.EmployeeId == id);

            if (emp == null)
            {
                throw new Exception("No Employee Found");
            }
            Console.WriteLine($"Employee ID : {emp.EmployeeId}");
            Console.WriteLine($"Name : {emp.FullName}");
            Console.WriteLine($"Email : {emp.Email}");
            Console.WriteLine($"Position : {emp.Position}");
            Console.WriteLine($"Department : {emp.Department}");
            Console.WriteLine($"Salary : {emp.Salary}");
            Console.WriteLine($"Hire Date : {emp.HireDate}");
            Console.WriteLine($"Created At : {emp.CreatedAt}");
            Console.WriteLine($"Phone Number : {emp.PhoneNumber}");
            Console.WriteLine($"Manager Name : {emp.ManagerName}");
            Console.WriteLine($"Status : {(emp.IsActive ? "Active" : "Inactive")}");
        }
        public void SearchByName(string name)
        {
            var employeesFound = employees
                .Where(x => x.FullName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (employeesFound.Count == 0)
            {
                throw new Exception("No Employee Found");
            }

            foreach (var emp in employeesFound)
            {
                Console.WriteLine($"Employee ID : {emp.EmployeeId}");
                Console.WriteLine($"Name : {emp.FullName}");
                Console.WriteLine($"Email : {emp.Email}");
                Console.WriteLine($"Position : {emp.Position}");
                Console.WriteLine($"Department : {emp.Department}");
                Console.WriteLine($"Salary : {emp.Salary}");
                Console.WriteLine($"Hire Date : {emp.HireDate}");
                Console.WriteLine($"Status : {(emp.IsActive ? "Active" : "Inactive")}");
                Console.WriteLine("-----------------------------");
            }
        }

        public void Filter_by_Department(Department department)
        {
            var emps = employees.FindAll(x => x.Department == department && x.IsActive);

            if (emps.Count == 0)
            {
                throw new Exception("No Employee Found");
            }

            foreach (var emp in emps)
            {
                Console.WriteLine($"Employee ID : {emp.EmployeeId}");
                Console.WriteLine($"Name : {emp.FullName}");
                Console.WriteLine($"Email : {emp.Email}");
                Console.WriteLine($"Position : {emp.Position}");
                Console.WriteLine($"Department : {emp.Department}");
                Console.WriteLine($"Salary : {emp.Salary}");
                Console.WriteLine($"Hire Date : {emp.HireDate}");
                Console.WriteLine($"Status : {(emp.IsActive ? "Active" : "Inactive")}");
                Console.WriteLine("-----------------------------");
            }



        }

        public void SortBySalaryAscending()
        {
            var emps = employees
                .OrderBy(x => x.Salary)
                .ToList();

            foreach (var emp in emps)
            {
                Console.WriteLine($"Employee ID : {emp.EmployeeId}");
                Console.WriteLine($"Name : {emp.FullName}");
                Console.WriteLine($"Salary : {emp.Salary}");
                Console.WriteLine($"Department : {emp.Department}");
                Console.WriteLine($"Hire Date : {emp.HireDate}");
                Console.WriteLine($"Status : {(emp.IsActive ? "Active" : "Inactive")}");
                Console.WriteLine("-----------------------------");
            }
        }

        public void SortBySalaryDescending()
        {
            var emps = employees.OrderByDescending(x => x.Salary).ToList();

            foreach (var emp in emps)
            {
                Console.WriteLine($"Employee ID : {emp.EmployeeId}");
                Console.WriteLine($"Name : {emp.FullName}");
                Console.WriteLine($"Salary : {emp.Salary}");
                Console.WriteLine($"Department : {emp.Department}");
                Console.WriteLine($"Hire Date : {emp.HireDate}");
                Console.WriteLine($"Status : {(emp.IsActive ? "Active" : "Inactive")}");
                Console.WriteLine("-----------------------------");
            }
        }
        public void SortByHireDateAscending()
        {
            var emps = employees
                .OrderBy(x => x.HireDate)
                .ToList();

            foreach (var emp in emps)
            {
                Console.WriteLine($"Employee ID : {emp.EmployeeId}");
                Console.WriteLine($"Name : {emp.FullName}");
                Console.WriteLine($"Salary : {emp.Salary}");
                Console.WriteLine($"Department : {emp.Department}");
                Console.WriteLine($"Hire Date : {emp.HireDate}");
                Console.WriteLine($"Status : {(emp.IsActive ? "Active" : "Inactive")}");
                Console.WriteLine("-----------------------------");
            }
        }
        public void SortByHireDateDescending()
        {
            var emps = employees.OrderByDescending(x => x.HireDate).ToList();

            foreach (var emp in emps)
            {
                Console.WriteLine($"Employee ID : {emp.EmployeeId}");
                Console.WriteLine($"Name : {emp.FullName}");
                Console.WriteLine($"Salary : {emp.Salary}");
                Console.WriteLine($"Department : {emp.Department}");
                Console.WriteLine($"Hire Date : {emp.HireDate}");
                Console.WriteLine($"Status : {(emp.IsActive ? "Active" : "Inactive")}");
                Console.WriteLine("-----------------------------");
            }
        }

        public void SortByName()
        {
            var emps = employees.OrderBy(x => x.FullName).ToList();

            foreach (var emp in emps)
            {
                Console.WriteLine($"Employee ID : {emp.EmployeeId}");
                Console.WriteLine($"Name : {emp.FullName}");
                Console.WriteLine($"Salary : {emp.Salary}");
                Console.WriteLine($"Department : {emp.Department}");
                Console.WriteLine($"Hire Date : {emp.HireDate}");
                Console.WriteLine($"Status : {(emp.IsActive ? "Active" : "Inactive")}");
                Console.WriteLine("-----------------------------");
            }
        }


        public void ViewAllEmployees()
        {
            foreach (var emp in employees)
            {
                Console.WriteLine($"Employee ID : {emp.EmployeeId}");
                Console.WriteLine($"Name : {emp.FullName}");
                Console.WriteLine($"Email : {emp.Email}");
                Console.WriteLine($"Position : {emp.Position}");
                Console.WriteLine($"Department : {emp.Department}");
                Console.WriteLine($"Salary : {emp.Salary}");
                Console.WriteLine($"Hire Date : {emp.HireDate}");
                Console.WriteLine($"Status : {(emp.IsActive ? "Active" : "Inactive")}");
                Console.WriteLine("-----------------------------");
            }
        }


    }


}
