using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        /*public static async void EnsurePopulated(IApplicationBuilder app) */      //This used for environment = development
            public static async Task EnsurePopulated(UserManager<IdentityUser> userManager)     //this using fo environment = production
        {
            //UserManager<IdentityUser> userManager = app.ApplicationServices       //This used for environment = development
            //    .GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser user = await userManager.FindByIdAsync(adminUser);

            if (user == null)
            {
                user = new IdentityUser("Admin");
                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}
