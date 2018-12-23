using EaShop.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EaShop.Api
{
    public static class Seed
    {
        public static void EnsureDataSeeded(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                SeedRelational(scope).GetAwaiter().GetResult();
            }
        }

        private static async Task SeedRelational(IServiceScope scope)
        {
            try
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                if (await roleManager.FindByNameAsync("Admin") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                if (!userManager.Users.Any())
                {
                    var user = new ApplicationUser
                    {
                        UserName = "test",
                        Email = "test@mail.com",
                        Name = "Test",
                        Surname = "Test"
                    };
                    await userManager.CreateAsync(user, "Password12$");
                    var admin = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@mail.com",
                        Name = "Admin",
                        Surname = "Admin"
                    };
                    await userManager.CreateAsync(admin, "Password12$");
                }

                var dbUser = await userManager.FindByNameAsync("test");

                if (!await userManager.IsInRoleAsync(dbUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(dbUser, "Admin");
                }

                dbUser = await userManager.FindByNameAsync("admin");

                if (!await userManager.IsInRoleAsync(dbUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(dbUser, "Admin");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}