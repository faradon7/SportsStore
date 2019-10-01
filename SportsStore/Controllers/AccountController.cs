using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SportsStore.Models.ViewModels;
using SportsStore.Models;
using SportsStore.Infrastructure;


namespace SportsStore.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<ApplicationRole> roleManager;

        public AccountController(UserManager<ApplicationUser> userMgr,
            SignInManager<ApplicationUser> signInMgr, RoleManager<ApplicationRole> roleMngr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMngr;
            IdentitySeedData.EnsurePopulated(userMgr, roleMngr).Wait();       //only for enviroment = production
        }

        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }
        
        [AllowAnonymous]
        public ViewResult LoginREST(string returnUrl)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = 
                    await userManager.FindByNameAsync(loginModel.Name);

                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded)
                    {
                        if ((await userManager.IsInRoleAsync(user, Roles.Admin)))
                        { 
                            return Redirect(loginModel?.ReturnUrl ?? "/Admin/Index");
                        }
                        return Redirect(loginModel?.ReturnUrl ?? "/");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid name or password");

            return View(loginModel);
        }

        [Authorize]
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
            => View(new SignUpViewModel());

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                await signInManager.SignOutAsync();

                var result = await userManager.FindByEmailAsync(model.Email);
                if (result == null)
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        Email = model.Email
                    };

                    if ((await userManager.CreateAsync(user, model.Password))
                        .Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, Roles.User);
                        await signInManager.PasswordSignInAsync(user,
                            model.Password, false, false);

                        return Redirect("/Profile/Index");
                    }
                }
            }
            var error = $"A user with the email {model.Email} is already registered.";
            ModelState.AddModelError("", error);
            TempData["message"] = error;
            return View(model);
        }
    }
}