using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Employee Number is required")]
        [MaxLength(4,ErrorMessage ="Length of the employee number should be 4")]
        [MinLength(4, ErrorMessage = "Length of the employee number should be 4")]
        public string EmployeeNumber { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Range(1,100,ErrorMessage ="Invalid department selection")]
        public int DepartmentId { get; set; }
        public string  DepartmentName { get; set; } 
    }
}
