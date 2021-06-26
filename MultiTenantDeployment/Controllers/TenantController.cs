using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiTenantDeployment.Repositories;
using MultiTenantDeployment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantDeployment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        ITenantReourceDeploymentRepository _tenantReourceDeploymentRepository;
        public TenantController(ITenantReourceDeploymentRepository tenantReourceDeploymentRepository)
        {
            _tenantReourceDeploymentRepository = tenantReourceDeploymentRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Deploy(TenantViewModel tenant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _tenantReourceDeploymentRepository.DeployTenantAsync(tenant);
                    return Ok(result);
                }
                else
                {
                    var errors = ModelState.Values;
                    return BadRequest(errors);
                }
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}
