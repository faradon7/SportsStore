﻿using System.Linq;
using SportsStore.Models;
using Xunit;

namespace SportStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void CanAddNewLines()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            //Arrange (creating new cart)
            Cart target = new Cart();

            //Action
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] result = target.Lines.ToArray();

            //Assert
            Assert.Equal(2, result.Length);
            Assert.Equal(p1, result[0].Product);
            Assert.Equal(p2, result[1].Product);
        }

        [Fact]
        public void CanAddQuantityForExistingLines()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();
            
            //Action
            target.AddItem(p1, 3);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            CartLine[] result = target.Lines.ToArray();

            //Assert
            Assert.Equal(2, result.Length);
            Assert.Equal(13, result[0].Quantity);
            Assert.Equal(1, result[1].Quantity);
        }

        [Fact]
        public void CanRemoveLine()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            Cart target = new Cart();
            target.AddItem(p1, 2);
            target.AddItem(p2, 1);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            //Act
            target.RemoveLine(p2);

            //Assert
            Assert.Equal(2, target.Lines.Count());
            Assert.Empty(target.Lines.Where(l => l.Product == p2));
        }

        [Fact]
        public void CalculateCartTotal()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            //Act
            target.AddItem(p1, 3);
            target.AddItem(p2, 2);

            decimal result = target.ComputeTotalValue();

            //Assert
            Assert.Equal(400M, result);
            
        }

        [Fact]
        public void CanClearContents()
        {
            //Arrange
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();
            target.AddItem(p1, 3);
            target.AddItem(p2, 2);

            //Act
            target.Clear();

            //Assert
            Assert.Empty(target.Lines);
        }
    }
}
