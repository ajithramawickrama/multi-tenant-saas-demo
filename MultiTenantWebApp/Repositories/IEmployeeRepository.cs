
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiTenantWebApp.Models;

namespace MultiTenantWebApp.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<EmployeeViewModel>> GetAllAsync();
        public  Task<EmployeeViewModel> GetAsync(int id);
        public  Task<EmployeeViewModel> AddAsync(EmployeeViewModel input);
        public  Task<EmployeeViewModel> EditAsync(EmployeeViewModel input);

        public  Task DeleteAsync(int id);
    }
}
