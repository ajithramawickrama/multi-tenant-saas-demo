using MultiTenantDeployment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantDeployment.Repositories
{
    public interface ITenantReourceDeploymentRepository
    {
        Task<bool> DeployTenantAsync(TenantViewModel tenantViewModel);
    }
}
