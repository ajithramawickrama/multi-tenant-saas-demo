using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantDeployment.Models
{
    public class CloudConfigurations
    {
        public string DefaultSubscriptionId { get; set; }
        public string DefaultSQLServerName { get; set; }
        public string DefaultElasticPoolName { get; set; }
        public string DefaultAppServicePlanName { get; set; }
        public string DefaultSQLServerUserId { get; set; }
        public string DefaultSQLServerPassword { get; set; }
        public string DefaultResourceGroupName { get; set; }
        public string StorageKey { get; set; }
        public string DatabaseFilePath { get; set; }  
        public string AppId { get; set; }
        public string TenantId { get; set; }
        public string ClientSecret { get; set; }
    }
}
