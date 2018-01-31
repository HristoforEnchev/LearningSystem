namespace LearningSystem.Web.Infrastructure.Extensions
{
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<LearningSystemDbContext>().Database.Migrate();


                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();


                Task.Run(async() => 
                {
                    var roles = new[]
                    {
                        WebConstants.AdministratorRole,
                        WebConstants.BlogAuthorRole,
                        WebConstants.TrainerRole
                    };

                    foreach (var role in roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role);

                        if (!roleExists)
                        {
                            await roleManager.CreateAsync(new IdentityRole { Name = role });
                        }
                    }
                   
                    var adminEmail = "admin@mysite.com";

                    var adminUser = await userManager.FindByEmailAsync(adminEmail); 

                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            Email = adminEmail,
                            UserName = WebConstants.AdministratorRole,
                            Name = WebConstants.AdministratorRole,
                            Birthdate = DateTime.Now
                        };

                        await userManager.CreateAsync(adminUser, "admin12");

                        await userManager.AddToRoleAsync(adminUser, WebConstants.AdministratorRole);
                    }
                })
                .GetAwaiter()
                .GetResult();
            }

            return app;
        }
    }
}
