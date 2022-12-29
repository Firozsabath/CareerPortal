using CUDJobApiIdentity.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Data
{
    public class SeedData
    {
        public async static Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }
        private async static Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if(await userManager.FindByEmailAsync("admin@cud.ac.ae") == null)
            {
                var user = new IdentityUser
                {
                    UserName = "admin@cud.ac.ae",
                    Email = "admin@cud.ac.ae"
                };
                var result = await userManager.CreateAsync(user, "Password11!");
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }

        }
        private async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Employer"))
            {
                var role = new IdentityRole
                {
                    Name = "Employer"
                };
                await roleManager.CreateAsync(role);
            }
            if (!await roleManager.RoleExistsAsync("Stundent"))
            {
                var role = new IdentityRole
                {
                    Name = "Stundent"
                };
                await roleManager.CreateAsync(role);
            }
        }

    }
}
