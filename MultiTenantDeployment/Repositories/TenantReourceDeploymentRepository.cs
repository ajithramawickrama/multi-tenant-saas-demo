using AutoMapper;
using Microsoft.Extensions.Options;
using MultiTenantDeployment.Models;
using MultiTenantDeployment.ViewModels;
using MutiTenantData.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;

namespace MultiTenantDeployment.Repositories
{
    public class TenantReourceDeploymentRepository : ITenantReourceDeploymentRepository
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configurations;
        private readonly IOptions<CloudConfigurations> _options;

        public TenantReourceDeploymentRepository(CatalogDbContext context, IMapper mapper, IConfigurationProvider configurations,IOptions<CloudConfigurations> options)
        {
            _context = context;
            _mapper = mapper;
            _configurations = configurations;
            _options = options;
        }
        public async Task<bool> DeployTenantAsync(TenantViewModel tenantViewModel)
        {
            var extTenant = _context.Tenants.Where(t => t.DomainName == tenantViewModel.DomainName).FirstOrDefault();
            if (extTenant != null)
                throw new Exception("Given domain name already exits");

            var tenant=_mapper.Map<Tenant>(tenantViewModel);            
            tenant.AppServicePlanName = _options.Value.DefaultAppServicePlanName;
            tenant.DbName = tenant.DomainName;
            tenant.ElasticPoolName = _options.Value.DefaultElasticPoolName;
            tenant.DbServerName = _options.Value.DefaultSQLServerName;
            tenant.ResourceGroupId = _options.Value.DefaultResourceGroupName;
            tenant.UserId = _options.Value.DefaultSQLServerUserId;
            tenant.Password = _options.Value.DefaultSQLServerUserId;
            tenant.DomainName = tenant.DomainName.ToLower();
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();

            await DeployToAzure(tenant);

            return true;
        }

        private async Task DeployToAzure(Tenant tenant)
        {
            try
            {
                var deploymentName = SdkContext.RandomResourceName("dpSAAS", 24);

                // Authenticate Azure account with service principal created
                var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(_options.Value.AppId, _options.Value.ClientSecret, _options.Value.TenantId, AzureEnvironment.AzureGlobalCloud);

                var azure = Azure
                  .Configure()
                  .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                  .Authenticate(credentials)
                  .WithDefaultSubscription();

                if (azure != null)
                {
                    try
                    {
                        var armTemplate = GetARMTemplate(tenant);

                        var result = await azure.Deployments.Define(deploymentName)
                          .WithExistingResourceGroup(_options.Value.DefaultResourceGroupName)
                          .WithTemplate(armTemplate)
                          .WithParameters("{}")
                          .WithMode(DeploymentMode.Incremental)
                          .CreateAsync();
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Deployment failed");
                    }
                }
                else
                {
                    throw new Exception("Login failed");
                }
                       
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private string GetARMTemplate(Tenant tenant)
        {
            var armTemplateString = File.ReadAllText(Path.Combine(".", "ARM", "ARMTempalate.json"));
            var parsedTemplate = JObject.Parse(armTemplateString);

            parsedTemplate.SelectToken("parameters.databaseName")["defaultValue"] = tenant.DbName;
            parsedTemplate.SelectToken("parameters.sqlServerName")["defaultValue"] =_options.Value.DefaultSQLServerName;
            parsedTemplate.SelectToken("parameters.sqlAdminUserName")["defaultValue"] = _options.Value.DefaultSQLServerUserId;
            parsedTemplate.SelectToken("parameters.sqlAdminPassword")["defaultValue"] = _options.Value.DefaultSQLServerPassword;
            parsedTemplate.SelectToken("parameters.elasticPoolId")["defaultValue"] = "/subscriptions/" + _options.Value.DefaultSubscriptionId + "/resourceGroups/" + _options.Value.DefaultResourceGroupName + "/providers/Microsoft.Sql/servers/" + _options.Value.DefaultSQLServerName + "/elasticpools/" + _options.Value.DefaultElasticPoolName; 
            parsedTemplate.SelectToken("parameters.domainName")["defaultValue"] = "yourdomain.com";
            parsedTemplate.SelectToken("parameters.tenantName")["defaultValue"] = tenant.DomainName;
            parsedTemplate.SelectToken("parameters.storageKey")["defaultValue"] = _options.Value.StorageKey;
            parsedTemplate.SelectToken("parameters.baseDatabasePath")["defaultValue"] = _options.Value.DatabaseFilePath;

            return parsedTemplate.ToString();
        }



    }
}
