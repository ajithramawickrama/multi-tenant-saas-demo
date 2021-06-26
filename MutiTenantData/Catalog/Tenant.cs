using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MutiTenantData.Catalog
{
    public class Tenant
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string DomainName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string DbServerName { get; set; }
        [Required]
        public string DbName { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ResourceGroupId { get; set; }
        [Required]
        public string ElasticPoolName { get; set; }
        [Required]
        public string AppServicePlanName { get; set; }


    }
}
