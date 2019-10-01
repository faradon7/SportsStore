using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using SportsStore.Models;
using Microsoft.AspNetCore.Authorization;
using SportsStore.Infrastructure;
using System.Linq;
using SportsStore.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SportsStore.Controllers.API_Controllers
{
    [Route("api/[controller]")]
    public class AccountApiController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<ApplicationRole> roleManager;

        public AccountApiController(UserManager<ApplicationUser> userMgr,
            SignInManager<ApplicationUser> signInMgr, RoleManager<ApplicationRole> roleMngr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMngr;
            IdentitySeedData.EnsurePopulated(userMgr, roleMngr).Wait();       //only for enviroment = production
        }

        [HttpGet]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        public StatusCodeResult Login([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = userManager.Users
                    .FirstOrDefault(u => u.UserName == loginModel.Name);

                if (user != null)
                {
                    signInManager.SignOutAsync();
                    if ((signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Result.Succeeded)
                    {
                        if ((userManager.IsInRoleAsync(user, Roles.Admin)).Result)
                        {
                            return Ok();
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");

            return NotFound();
        }

    }
}
