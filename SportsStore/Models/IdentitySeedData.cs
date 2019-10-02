using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SportsStore.Infrastructure;

namespace SportsStore.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Secret123$";

        public static async Task EnsurePopulated(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Adding Roles
            if (await roleManager.FindByNameAsync(Roles.Admin) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(Roles.Admin, Roles.AdminDescription));
            }

            if (await roleManager.FindByNameAsync(Roles.User) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(Roles.User, Roles.UserDecription));
            }

            if ((await userManager.GetUsersInRoleAsync(Roles.Admin)).Count == 0)
            {
                ApplicationUser admin = new ApplicationUser(adminUser);

                var result = await userManager.CreateAsync(admin);

                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(admin, adminPassword);
                    await userManager.AddToRoleAsync(admin, Roles.Admin);
                }
            }
        }
    }
}
