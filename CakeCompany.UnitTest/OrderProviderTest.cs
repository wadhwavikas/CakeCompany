using System;
using NUnit.Framework;
using CakeCompany.Provider;
using CakeCompany.Models;

namespace CakeCompany.UnitTest
{
    [TestFixture]
    public class OrderProviderTest
    {
        [Test]
        public void GetCancelledOrders_Should_Return_Cancelled_Orders()
        {
            //Arrange
            var orders = new Order[]
            {
                new("CakeBox", DateTime.Now, 1, Cake.RedVelvet, 10),
                new("ImportantCakeShop", DateTime.Now, 2, Cake.RedVelvet, 50),
                new("RedVelvet", DateTime.Now.AddHours(2), 3, Cake.RedVelvet, 100)
            };

            OrderProvider orderProvider = new OrderProvider();

            //Act
            var result = orderProvider.GetCancelledOrders(orders);

            //Assert
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Exists(result => result.Id == 1 || result.Id == 2));
            Assert.IsTrue(result.Exists(result => result.Id != 3));
        }

        [Test]
        public void GetCancelledOrders_Should_Return_Cancelled_Orders_When_CakeType_Is_RedVelvet()
        {
            //Arrange
            var orders = new Order[]
            {
                new("CakeBox", DateTime.Now, 1, Cake.RedVelvet, 10),
                new("ImportantCakeShop", DateTime.Now.AddMinutes(65), 2, Cake.RedVelvet, 50)     
            };

            OrderProvider orderProvider = new OrderProvider();

            //Act
            var result = orderProvider.GetCancelledOrders(orders);

            //Assert
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Exists(result => result.Id == 1));
         
        }

        [Test]
        public void GetCancelledOrders_Should_Return_Cancelled_Orders_When_CakeType_Is_Chocolate()
        {
            //Arrange
            var orders = new Order[]
            {
                new("CakeBox", DateTime.Now, 1, Cake.Chocolate, 10),
                new("ImportantCakeShop", DateTime.Now.AddMinutes(35), 2, Cake.Chocolate, 50)
            };

            OrderProvider orderProvider = new OrderProvider();

            //Act
            var result = orderProvider.GetCancelledOrders(orders);

            //Assert
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Exists(result => result.Id == 1));

        }

        [Test]
        public void GetCancelledOrders_Should_Return_Cancelled_Orders_When_CakeType_Is_Vanilla()
        {
            //Arrange
            var orders = new Order[]
            {
                new("CakeBox", DateTime.Now, 1, Cake.Vanilla, 10),
                new("ImportantCakeShop", DateTime.Now.AddHours(16), 2, Cake.Vanilla, 50)
            };

            OrderProvider orderProvider = new OrderProvider();

            //Act
            var result = orderProvider.GetCancelledOrders(orders);

            //Assert
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Exists(result => result.Id == 1));

        }

        [Test]
        public void ProcessOrders_Should_Process_Eligible_Orders()
        {
            //Arrange
            var orders = new Order[]
            {
                new("CakeBox", DateTime.Now, 1, Cake.RedVelvet, 10),
                new("ImportantCakeShop", DateTime.Now, 2, Cake.RedVelvet, 50),
                new("RedVelvet", DateTime.Now.AddHours(2), 3, Cake.RedVelvet, 100)
            };

            OrderProvider orderProvider = new OrderProvider();

            //Act
            var result = orderProvider.ProcessOrder(orders);

            //Assert
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Exists(result => result.Quantity == 100));
            
        }

    }
}

