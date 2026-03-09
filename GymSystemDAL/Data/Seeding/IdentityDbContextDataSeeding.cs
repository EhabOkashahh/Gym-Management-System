using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymSystemDAL.Data.Seeding
{
    public static class IdentityDbContextDataSeeding
    {
        public static async Task<bool> SeedData(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            try
            {
                var hasUsers = userManager.Users.Any();
                var hasRoles = roleManager.Roles.Any();

                if (hasRoles && hasUsers) return false;

                if (!hasRoles)
                {
                    var roles = new List<IdentityRole>()
                    {
                        new() { Name = "SuperAdmin" },
                        new() { Name = "Admin" }
                    };

                    foreach (var role in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role.Name!))
                        {
                            await roleManager.CreateAsync(role);
                        }
                    }
                }

                if (!hasUsers)
                {
                    var admins = new List<AppUser>(){
                        new()
                        {
                            UserName = "Ehab",
                            Email = "okashahehab@gmail.com",
                            PhoneNumber = "01030370717",
                        },
                        new()
                        {
                            UserName = "Ehab_",
                            Email = "ehabyasser82@gmail.com",
                            PhoneNumber = "01030370719",
                        }
                    };

                    foreach (var item in admins)
                    {
                        await userManager.CreateAsync(item,"P@ssw0rd");
                        if (item.UserName == "Ehab") await userManager.AddToRoleAsync(item, "SuperAdmin");
                        else await userManager.AddToRoleAsync(item, "Admin");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAILED SEEDING USERS AND ROLES DATA {ex}");
                return false;
            }
        }
    }
}