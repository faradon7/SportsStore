using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using SportsStore.Models;
using Microsoft.AspNetCore.Authorization;
using SportsStore.Infrastructure;
using System.Linq;

namespace SportsStore.Controllers.API_Controllers
{
    //[Authorize(Roles = Roles.Admin)]
    public class AdminApiController : Controller
    {
        private IProductRepository repository;

        public AdminApiController(IProductRepository repo)
            => repository = repo;
        public ViewResult Index() => View();


        [HttpGet]
        [Route("api/[controller]")]
        public IQueryable<Product> Get() => repository.Products;

        public ViewResult Edit(int productId) =>
            View(repository.Products
                .FirstOrDefault(p => p.ProductID == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                //there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create() => View("Edit", new Product());

        [Route("api/[controller]")]
        [HttpDelete("{Id}")]
        public StatusCodeResult Delete(int Id)
        {
            Product deletedProduct = repository.DeleteProduct(Id);
            if (deletedProduct != null)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);

            return RedirectToAction(nameof(Index));
        }
    }
}
