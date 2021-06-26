using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MutiTenantData.Application
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }   
    }
}
