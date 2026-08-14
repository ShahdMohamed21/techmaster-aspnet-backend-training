using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }
        public string Position { get; set; }
        public decimal Salary{ get; set; }

        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public string ManagerName { get; set; }

        public Employee()
        {

        }


    }

}

