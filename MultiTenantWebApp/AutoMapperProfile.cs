using AutoMapper;
using MultiTenantWebApp.Models;
using MutiTenantData.Application;

namespace MultiTenantWebApp
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
