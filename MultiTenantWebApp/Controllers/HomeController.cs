using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MultiTenantWebApp.Models;
using MutiTenantData.Application;
using MutiTenantData.Catalog;
using MultiTenantWebApp.Repositories;

namespace MultiTenantWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITenantInfoRepository _tenantInfoRepository;

        public HomeController(ILogger<HomeController> logger, ITenantInfoRepository tenantInfoRepository)
        {
            _logger = logger;
            _tenantInfoRepository = tenantInfoRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            var tenant = _tenantInfoRepository.GetTenant(HttpContext.Request.Host.Host.ToLower());
            if (tenant != null)
                ViewBag.CompanyName = tenant.CompanyName;
            else
                throw new Exception("Invalid Tenant");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
