using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileRepository repository;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<ApplicationRole> roleManager;
        private string appUserId { get; set; }
        public ProfileController(IProfileRepository repo, UserManager<ApplicationUser> userMgr,
            SignInManager<ApplicationUser> signInMgr, RoleManager<ApplicationRole> roleMngr)
        {
            repository = repo;
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMngr;
        }
        public async Task<IActionResult> Index()
        {
            //var user = await userManager
            //    .FindByNameAsync(User.Identity.Name);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            appUserId = userId;

            var profile = repository.Profiles
                .FirstOrDefault(p => p.ApplicationUserID == userId);

            if (profile == null)
            {
                return View();
            }
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSettings(CustomerProfile profile)
        {
            if (ModelState.IsValid)
            {
                //await userManager.FindByNameAsync(profile)
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                profile.ApplicationUserID = userId;

                repository.SaveProfile(profile);
            }
            return Redirect(nameof(Index));
        }
    }
}