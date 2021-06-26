using AutoMapper;
using MultiTenantDeployment.ViewModels;
using MutiTenantData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantDeployment
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TenantViewModel, Tenant>().ReverseMap();
        }
    }
}
