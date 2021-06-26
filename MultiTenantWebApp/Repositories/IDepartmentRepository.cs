
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiTenantWebApp.Models;

namespace MultiTenantWebApp.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<DepartmentViewModel> GetDepartments();
    }
}
