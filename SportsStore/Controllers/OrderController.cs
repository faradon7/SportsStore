using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsStore.Models;
using System.Linq;
using SportsStore.Infrastructure;
using System.Security.Claims;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        #region private properties

        private IOrderRepository repository;
        private IProfileRepository profileRepository;
        private Cart cart;

        #endregion

        public OrderController(IOrderRepository repoService,
            IProfileRepository profileService, Cart cartService)
        {
            repository = repoService;
            cart = cartService;
            profileRepository = profileService;
        }

        [Authorize(Roles = Roles.Admin)]
        public ViewResult List() => View(repository.Orders.Where(o => !o.Shipped));

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult MarkShipped(int orderID)
        {
            Order order = repository.Orders.FirstOrDefault(o => o.ID == orderID);

            if (order != null)
            {
                order.Shipped = true;
                repository.SaveOrder(order);
            }

            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = profileRepository.Profiles
                .FirstOrDefault(p => p.ApplicationUserID == userId);

            if (profile?.Location == null)
            {
                return View(new Order());
            }

            return View(new Order
            {
                LocationID = profile.Location.ID,
                Location = profile.Location
            });
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);

                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            cart.Clear();

            return View();
        }
    }
}