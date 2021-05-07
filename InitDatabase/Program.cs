using AspCoreMVC_Starter.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InitDatabase
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            MyConfiguration.ConfigureServices(serviceCollection);

            using var serviceProvider = serviceCollection.BuildServiceProvider();

            ApplicationDbContext context = serviceProvider.GetService<ApplicationDbContext>();
            UserManager<IdentityUser> userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            await RunPendingMigrations(context);
            await CreateDefaultRoles(roleManager);
            await CreateDefaultAdmin(userManager);
        }

        private static async Task CreateDefaultAdmin(UserManager<IdentityUser> userManager)
        {
            // TODO: Create your default admin. Ensure the password meets your requirements!
            const string email = "admin@yourwebsite.com";
            const string username = "Admin";
            const string password = "Programming01#";

            // Do nothing if admin is already created
            if (await userManager.FindByEmailAsync(email) != null)
            {
                return;
            }

            var adminUser = new IdentityUser()
            {
                Email = email,
                UserName = username,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(adminUser, password);
            await userManager.AddToRoleAsync(adminUser, IdentityHelper.AdminRole);
        }

        private static async Task RunPendingMigrations(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();
        }

        private static async Task CreateDefaultRoles(RoleManager<IdentityRole> roleManager)
        {
            IEnumerable<string> roles = IdentityHelper.GetRoles();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}