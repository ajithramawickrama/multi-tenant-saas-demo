using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using MultiTenantWebApp.Models;
using MutiTenantData.Application;

namespace MultiTenantWebApp.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DepartmentRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public  IEnumerable<DepartmentViewModel> GetDepartments()
        {
            var departments = _context.Departments.ToList();
            return _mapper.Map<List<DepartmentViewModel>>(departments);
        }
    }
}
