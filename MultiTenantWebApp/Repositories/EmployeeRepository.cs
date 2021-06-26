
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using AutoMapper.QueryableExtensions;
using MultiTenantWebApp.Models;
using MutiTenantData.Application;

namespace MultiTenantWebApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configurations;
        public EmployeeRepository(ApplicationDbContext context,IMapper mapper,IConfigurationProvider configurations)
        {
            _context = context;
            _mapper = mapper;
            _configurations = configurations;
        }

        public async Task<EmployeeViewModel> AddAsync(EmployeeViewModel input)
        {
            var employee = _mapper.Map<Employee>(input);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            input.Id = employee.Id;
            return input;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.Where(t => t.Id == id).FirstOrDefaultAsync();
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Invalid Employee Record");
            }
        }

        public async Task<EmployeeViewModel> EditAsync(EmployeeViewModel input)
        {
            var employee = _mapper.Map<Employee>(input);
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return input;
        }

        public async Task<EmployeeViewModel> GetAsync(int id)
        {
            var employee= await _context.Employees.Where(t => t.Id == id).Include(e => e.Department).ProjectTo<EmployeeViewModel>(_configurations).FirstOrDefaultAsync();
            var employeeToReturn = _mapper.Map<EmployeeViewModel>(employee);
            return employeeToReturn;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAllAsync()
        {
            var employees = await _context.Employees.Include(e => e.Department).ProjectTo<EmployeeViewModel>(_configurations).ToListAsync();
            return employees;
        }
    }
}
