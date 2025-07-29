using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Suwen.Persistence.Seeding
{
    public static class DataSeeder
    {
        /*public static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                string[] roleNames = { "Admin", "User" };

                foreach (var roleName in roleNames)
                {
                    var roleExists = await roleManager.RoleExistsAsync(roleName);

                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new AppRole { Name = roleName, CreatedTime = DateTime.UtcNow });
                        Console.WriteLine($"[DataSeeder]Rol '{roleName}' created successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"[DataSeeder]Role '{roleName}' already exists.");
                    }
                }

                var adminUserMail = configuration["AdminUser:Email"] ?? "";
                var adminUserPassword = configuration["AdminUser:Password"] ?? "";

                var adminUser = await userManager.FindByEmailAsync(adminUserMail);

                if (adminUser == null)
                {
                    var newAdminUser = new AppUser
                    {
                        UserName = "Admin",
                        Email = adminUserMail,
                        FirstName = "Yagmur",
                        LastName = "Aslan",
                        EmailConfirmed = true,
                        CreatedDate = DateTime.UtcNow
                    };
                    var createAdminUserResult = await userManager.CreateAsync(newAdminUser, adminUserPassword);

                    if (createAdminUserResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAdminUser, "Admin");
                        await userManager.AddToRoleAsync(newAdminUser, "User");
                        Console.WriteLine($"[DataSeeder]Admin user '{newAdminUser.UserName}' created and added to roles successfully.");
                    }
                    else
                    {
                        var errors = string.Join(", ", createAdminUserResult.Errors.Select(e => e.Description));
                        Console.WriteLine($"[DataSeeder]Failed to create admin user: {errors}");
                    }

                }
                else
                {
                        Console.WriteLine($"[DataSeeder]Admin user '{adminUser.UserName}' already exists.");
                }
            }
        }*/
    }
}
