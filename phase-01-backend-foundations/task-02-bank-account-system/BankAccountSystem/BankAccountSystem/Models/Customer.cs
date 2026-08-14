using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02.BankAccountSystem.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }


        [Required]
        public string FullName { get; set; } = string.Empty;


        [Required]
        public string Email { get; set; } = string.Empty;


        [Required]
        public string PhoneNumber { get; set; } = string.Empty;


        public DateTime CreatedAt { get; set; }

        public Customer()
        {
        }
    }
}
