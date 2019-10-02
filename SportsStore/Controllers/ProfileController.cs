using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class ProfileController : Controller
    {
        #region private properties

        private IProfileRepository repository;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<ApplicationRole> roleManager;

        #endregion

        public ProfileController(IProfileRepository repo, UserManager<ApplicationUser> userMgr,
            SignInManager<ApplicationUser> signInMgr, RoleManager<ApplicationRole> roleMngr)
        {
            repository = repo;
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMngr;
        }

        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profile = repository.Profiles
                .FirstOrDefault(p => p.ApplicationUserID == userId);

            if (profile == null)
            {
                return View();
            }

            return View(profile);
        }

        [HttpPost]
        public IActionResult SaveSettings(CustomerProfile profile)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                profile.ApplicationUserID = userId;

                repository.SaveProfile(profile);
            }

            return Redirect(nameof(Index));
        }
    }
}