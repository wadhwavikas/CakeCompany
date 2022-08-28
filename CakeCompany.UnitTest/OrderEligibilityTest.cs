using System;
using CakeCompany.Models;
using CakeCompany.Provider;
using Moq;
using System.Collections.Generic;
using NUnit.Framework;
using CakeCompany.Facade;

namespace CakeCompany.UnitTest
{
    [TestFixture]
    public class OrderEligibilityTest
    {
        [Test]
        public void isEligible_Should_Return_False_When_EsitimatedBakeTime_IsGreaterThan_EstimatedDeliveryTime()
        {
            //Arrange

            var mockCakeProvider = new Mock<ICakeProvider>();
            mockCakeProvider.Setup(x => x.Check(It.IsAny<Order>())).Returns(DateTime.Now.AddHours(1));

            var orders = new Order[]
            { new("CakeBox", DateTime.Now, 1, Cake.RedVelvet, 10), };

            //Act

            OrderEligibility orderEligibility = new OrderEligibility(mockCakeProvider.Object, new PaymentProvider());
            var result = orderEligibility.isEligible(orders[0]);

            //Assert

            Assert.AreEqual(result, false);
        }

        [Test]
        public void isEligible_Should_Return_False_When_Payment_Failed()
        {
            //Arrange

            var mockCakeProvider = new Mock<ICakeProvider>();
            mockCakeProvider.Setup(x => x.Check(It.IsAny<Order>())).Returns(DateTime.Now);

            var payment = new PaymentIn { HasCreditLimit = false, IsSuccessful = false }; 

            var mockPaymentProvider = new Mock<IPaymentProvider>();
            mockPaymentProvider.Setup(x => x.Process(It.IsAny<Order>())).Returns(payment);

            var orders = new Order[]
            { new("CakeBox", DateTime.Now.AddHours(2), 1, Cake.RedVelvet, 10), };

            //Act

            OrderEligibility orderEligibility = new OrderEligibility(mockCakeProvider.Object, mockPaymentProvider.Object);
            var result = orderEligibility.isEligible(orders[0]);

            //Assert

            Assert.AreEqual(result, false);
        }

        [Test]
        public void isEligible_Should_Return_True_When_All_Checks_Passed()
        {
            //Arrange

            var mockCakeProvider = new Mock<ICakeProvider>();
            mockCakeProvider.Setup(x => x.Check(It.IsAny<Order>())).Returns(DateTime.Now);

            var payment = new PaymentIn { HasCreditLimit = false, IsSuccessful = true };

            var mockPaymentProvider = new Mock<IPaymentProvider>();
            mockPaymentProvider.Setup(x => x.Process(It.IsAny<Order>())).Returns(payment);

            var orders = new Order[]
            { new("CakeBox", DateTime.Now.AddHours(2), 1, Cake.RedVelvet, 10), };

            //Act

            OrderEligibility orderEligibility = new OrderEligibility(mockCakeProvider.Object, mockPaymentProvider.Object);
            var result = orderEligibility.isEligible(orders[0]);

            //Assert

            Assert.AreEqual(result, true);
        }

    }
}

