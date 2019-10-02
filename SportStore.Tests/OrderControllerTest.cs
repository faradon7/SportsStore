using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportStore.Tests
{
    public class OrderControllerTest
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange - creating MOck repo
            Mock<IOrderRepository> mockOrderRepo = new Mock<IOrderRepository>();
            Mock<IProfileRepository> mockProfileRepo = new Mock<IProfileRepository>();

            //Arrange create an empty cart
            Cart cart = new Cart();

            //Arrange create the order
            Order order = new Order();

            //Arrange create an instance of the controller
            OrderController target = new OrderController(mockOrderRepo.Object, 
                mockProfileRepo.Object, cart);

            //Act
            ViewResult result = target.Checkout(order) as ViewResult;

            //Assert - check that the order hasnt been stored
            mockOrderRepo.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            //Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            //Assert - check that I'm passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);

        }
        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            //Arrange - create a mock order repository
            Mock<IOrderRepository> mockOrderRepo = new Mock<IOrderRepository>();
            Mock<IProfileRepository> mockProfileRepo = new Mock<IProfileRepository>();

            //Arrange - create a cart with one item
            Cart cart = new Cart ();
            cart.AddItem(new Product(), 1);

            //Arrange - create an instance of the controller
            OrderController target = new OrderController(mockOrderRepo.Object,
                mockProfileRepo.Object, cart);

            //Arrange - add an error to the model
            target.ModelState.AddModelError("error", "error");

            //Act - try to checkout
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            //Assert - check that the order hasn't been passed stored
            mockOrderRepo.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            //Assert - check that the method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            //Assert - check that I'm passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            //Arrange create a mock order repository
            Mock<IOrderRepository> mockOrderRepo = new Mock<IOrderRepository>();
            Mock<IProfileRepository> mockProfileRepo = new Mock<IProfileRepository>();

            //Arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            //Arrange - crete an instance of the controller
            OrderController target = new OrderController(mockOrderRepo.Object, 
                mockProfileRepo.Object, cart);

            //Act - try to checkout
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            //Assert - check that the order has been stored
            mockOrderRepo.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

            //Assert - check the method redirected to the Completed action
            Assert.Equal("Completed", result.ActionName);

        }
    }
}
