using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using SportsStore.Models;
using Microsoft.AspNetCore.Authorization;
using SportsStore.Infrastructure;
using System.Linq;

namespace SportsStore.Controllers.API_Controllers
{
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

        [HttpPost("api/[controller]")]
        public Product Post([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return product;
            }
            else
            {
                return null;
            }
        }

        [HttpDelete("api/[controller]/{id}")]
        public StatusCodeResult Delete(int id)
        {
            Product deletedProduct = repository.DeleteProduct(id);
            if (deletedProduct != null)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
