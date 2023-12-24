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
                        UserName = adminEmail,
                        Email = adminEmail,
                        Ville = "____"
                    };

                    var createAdmin = await userManager.CreateAsync(adminUser, "Admin_001");
                    
                    if (createAdmin.Succeeded)
                    {
                        adminUser.Nom = "Admin";
                        adminUser.MyScore = "0";
                        adminUser.Prenom = "Admin";
                        adminUser.Ville = "_______";
                        adminUser.Adresse = "_______";
                        adminUser.Role = "admin";
                        await userManager.UpdateAsync(adminUser);
                        var token = await userManager.GenerateEmailConfirmationTokenAsync(adminUser);
                        await userManager.ConfirmEmailAsync(adminUser, token);

                    }

                    



                }

                

            }
        }


    }
}
