using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MutiTenantData.Catalog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Claims;

namespace MutiTenantData.Application
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly CatalogDbContext _catalogDbContext;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor, 
            IConfiguration configuration,
            CatalogDbContext catalogDbContext)
            : base(options)
        {
            _options = options;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _catalogDbContext = catalogDbContext;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    var domainName = _httpContextAccessor.HttpContext.Request.Host.Host.ToLower();
                    var tenantData = _catalogDbContext.Tenants.Where(t => t.DomainName == domainName).FirstOrDefault();
                    if (tenantData != null)
                    {
                        optionsBuilder.UseSqlServer($"Server={tenantData.DbServerName};" +
                            $"Database={tenantData.DbName}; " +
                            $"user Id={tenantData.UserId}; password={tenantData.Password}" +
                            $"Trusted_Connection=True;MultipleActiveResultSets=true");
                    }
                    else
                    {
                        throw new Exception("Invalid database");
                    }
                }
                else
                {
                    optionsBuilder.UseSqlServer(_configuration.GetConnectionString("EmployeeConnectionString"));
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User",schema:"Identity");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role", schema: "Identity");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", schema: "Identity");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", schema: "Identity");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", schema: "Identity");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", schema: "Identity");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", schema: "Identity");
            });

            builder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", schema: "Employee");
            });
            builder.Entity<Department>(entity =>
            {
                entity.ToTable("Department", schema: "Employee");
            });

            builder.SeedMasterData();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
