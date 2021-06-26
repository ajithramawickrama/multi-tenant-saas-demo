using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiTenantWebApp.Models;


namespace MutiTenantData.Application
{
    public static class SeedDefaultData
    {
        public static void SeedMasterData(this ModelBuilder builder)
        {
            builder.Entity<Department>().HasData(
               new Department
               {
                   Id = 1,
                   Name = "Humen Resource"
               },
               new Department
               {
                   Id = 2,
                   Name = "Information Technology"
               }
               );

        }

        public static async Task SeedAdminUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole { Name = Enums.Roles.SuperAdmin.ToString() });
            await roleManager.CreateAsync(new IdentityRole { Name = Enums.Roles.Admin.ToString() });
            await roleManager.CreateAsync(new IdentityRole { Name = Enums.Roles.NormalUser.ToString() });

            var adminUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Ajith",
                LastName = "Ramawickrama",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.UserName != adminUser.UserName))
            {
                var user = await userManager.FindByEmailAsync(adminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(adminUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(adminUser, Enums.Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}
