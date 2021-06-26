using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantDeployment.ViewModels
{
    public class TenantViewModel
    {

        [Required(ErrorMessage ="Company name is required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Domain name is required")]
        [MaxLength(20,ErrorMessage ="Domain name length should not greater than 20")]
        public string DomainName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        public string ContactNumber { get; set; }
    }
}
