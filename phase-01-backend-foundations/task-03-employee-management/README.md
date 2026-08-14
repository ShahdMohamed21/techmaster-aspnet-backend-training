# Employee Management Console App

A simple console-based Employee Management System built with C#.

This project is designed to practice collections, LINQ, searching, filtering, sorting, validation, and basic reporting.

## Features

### 1. Add Employee
Allows HR to add a new employee by entering:
- Full Name
- Email
- Department
- Position
- Salary
- Hire Date
- Phone Number
- Manager Name

The system:
- Generates a unique Employee ID automatically.
- Validates required fields.
- Ensures salary is greater than zero.
- Prevents future hire dates.
- Adds every new employee as Active.

### 2. Update Employee
Allows updating an existing employee using their Employee ID.

The following information can be updated:
- Email
- Department
- Position
- Salary

Employee ID remains unchanged.

The system validates that the employee exists and that the entered values are valid.

### 3. Deactivate Employee
Allows HR to deactivate an employee using their Employee ID.

The employee is not removed from the list. Instead:
- `IsActive` is set to `false`.
- The employee record remains stored.
- The system prevents deactivating an already inactive employee.

### 4. Search Employee
Employees can be searched using:

- Employee ID
- Full Name

Name search:
- Supports partial names.
- Is case-insensitive.

Example:

```text
Enter search value: ahmed
The system can find employees such as `Ahmed Tarek`.

### 5. Filter by Department
Allows the user to enter a department name and display employees from that department.

Example:

```text
Enter Department: IT

Department search is case-insensitive.

Only active employees are displayed by default.

Available departments:
- IT
- HR
- Sales
- Finance
- Marketing
- Support

### 6. Sort Employees
Employees can be sorted using different options:

1. Salary Ascending
2. Salary Descending
3. Hire Date Ascending
4. Hire Date Descending
5. Name

### 7. Salary Reports
The system provides:

- Average Salary
- Highest Salary Employee
- Lowest Salary Employee
- Total Payroll
- Employees Count by Department
- Active Employees Count
- Inactive Employees Count

### 8. View All Employees
Displays all employees with:

- Employee ID
- Name
- Email
- Position
- Department
- Salary
- Hire Date
- Status

### 9. Exit
Closes the application and displays a goodbye message.

## Project Structure

```text
EmployeeManagement/
│
├── Models/
│   └── Employee.cs
│
├── Services/
│   ├── EmployeeService.cs
│   └── EmployeeReportService.cs
│
├── UI/
│   └── ConsoleMenu.cs
│
└── Program.cs
