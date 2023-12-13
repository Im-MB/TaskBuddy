using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskBuddy.Models;

namespace TaskBuddy.Data
{
    public class ApplicationDbInitializer
    {


        

        public static async Task Seed(IApplicationBuilder applicationBuilder) 
        {


            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Utilisateur>>();

                var adminEmail = "admin@admin.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null)
                {
                    adminUser = new Utilisateur
                    {
                        UserName = "Admin",
                        Email = adminEmail,
                    };

                    var createAdmin = await userManager.CreateAsync(adminUser, "Admin_001");
                    if (createAdmin.Succeeded)
                    {
                        // Generate email confirmation token and confirm the email
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(adminUser);
                        await userManager.ConfirmEmailAsync(adminUser, token);

                    }

                }

                

            }
        }


    }
}
