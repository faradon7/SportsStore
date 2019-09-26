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
        public const string role_admin = "Administrator";
        public const string desc_admin = "Администратор сайта, имеет полный доступ";
        public const string role_user = "User";
        public const string desc_user = "Пользователь сайта, имеет доступ только к своему профилю";

        /*public static async void EnsurePopulated(IApplicationBuilder app) */      //This used for environment = development
        public static async Task EnsurePopulated(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)     //this using fo environment = production
        {
            //UserManager<IdentityUser> userManager = app.ApplicationServices       //This used for environment = development
            //    .GetRequiredService<UserManager<IdentityUser>>();

            //Adding Roles
            if (await roleManager.FindByNameAsync(role_admin) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role_admin, desc_admin));
            }

            if (await roleManager.FindByNameAsync(role_user) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(role_user, desc_user));
            }


            //ApplicationUser user = await userManager.FindByIdAsync(adminUser);

            if ((await userManager.GetUsersInRoleAsync(role_admin)).Count == 0)
            {
                ApplicationUser admin = new ApplicationUser(adminUser);

                var result = await userManager.CreateAsync(admin);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(admin, adminPassword);
                    await userManager.AddToRoleAsync(admin, role_admin);
                }
            }
        }
    }
}
