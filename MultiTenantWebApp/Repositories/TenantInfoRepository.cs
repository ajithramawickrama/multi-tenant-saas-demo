using Microsoft.AspNetCore.Http;
using MutiTenantData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantWebApp.Repositories
{
    public class TenantInfoRepository : ITenantInfoRepository
    {
        private readonly CatalogDbContext _catalogDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantInfoRepository(CatalogDbContext catalogDbContext,IHttpContextAccessor httpContextAccessor)
        {
            _catalogDbContext = catalogDbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public Tenant GetTenant(string domainName)
        {
            var tenant = _catalogDbContext.Tenants.Where(t => t.DomainName == _httpContextAccessor.HttpContext.Request.Host.Host.ToLower()).FirstOrDefault();
            if (tenant != null)
            {
                return tenant;
            }
            else
                throw new Exception("Invalid Tenant");
        }
    }
}
