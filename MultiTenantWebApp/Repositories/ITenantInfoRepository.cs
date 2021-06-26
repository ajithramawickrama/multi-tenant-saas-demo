using MutiTenantData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Repositories
{
    public interface ITenantInfoRepository
    {
        public Tenant GetTenant(string domainName);
    }
}
