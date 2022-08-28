using System;
using Moq;
using NUnit.Framework;
using CakeCompany.Provider;
using CakeCompany.Models;
using System.Collections.Generic;

namespace CakeCompany.UnitTest
{
    [TestFixture]
    public class ShipmentProviderTest
    {

        [Test]
        public void GetShipment_Should_Return_Empty_Shipment_When_OrdersList_Empty()
        {
            //Arrange

            var mockTransportProvider = new Mock<ITransportProvider>();
            mockTransportProvider.Setup(x => x.ProcessTranportation(It.IsAny<List<Product>>())).Returns(string.Empty);

            var mockOrderProvider = new Mock<IOrderProvider>();
            mockOrderProvider.Setup(x => x.GetLatestOrders()).Returns(new Order[0]);
            mockOrderProvider.Setup(x => x.ProcessOrder(It.IsAny<Order[]>())).Returns(new List<Product>());


            //Act

            IShipmentProvider shipment = new ShipmentProvider(mockOrderProvider.Object, mockTransportProvider.Object);
            var result = shipment.GetShipment();

            //Assert

            Assert.AreEqual(result, string.Empty);

        }

        [Test]
        public void GetShipment_Should_Return_Empty_Shipment_When_No_Eligible_Orders()
        {
            //Arrange

            var mockTransportProvider = new Mock<ITransportProvider>();
            mockTransportProvider.Setup(x => x.ProcessTranportation(It.IsAny<List<Product>>())).Returns(string.Empty);

            var mockOrderProvider = new Mock<IOrderProvider>();
            mockOrderProvider.Setup(x => x.GetLatestOrders()).Returns(new Order[]
                {
                    new("RedVelvet", DateTime.Now, 3, Cake.RedVelvet, 100)
                }
            );
            mockOrderProvider.Setup(x => x.ProcessOrder(It.IsAny<Order[]>())).Returns(new List<Product>());


            //Act

            IShipmentProvider shipment = new ShipmentProvider(mockOrderProvider.Object, mockTransportProvider.Object);
            var result = shipment.GetShipment();

            //Assert

            Assert.AreEqual(result, string.Empty);

        }

        [Test]
        public void GetShipment_Should_Return_Correct_Shipment_When_Eligible_Orders()
        {
            //Arrange

            var mockTransportProvider = new Mock<ITransportProvider>();
            mockTransportProvider.Setup(x => x.ProcessTranportation(It.IsAny<List<Product>>())).Returns("1234");

            var mockOrderProvider = new Mock<IOrderProvider>();
            mockOrderProvider.Setup(x => x.GetLatestOrders()).Returns(new Order[]
                {
                    new("RedVelvet", DateTime.Now.AddHours(2), 3, Cake.RedVelvet, 100)
                }
            );
            mockOrderProvider.Setup(x => x.ProcessOrder(It.IsAny<Order[]>())).Returns(new List<Product>());

            //Act

            IShipmentProvider shipment = new ShipmentProvider(mockOrderProvider.Object, mockTransportProvider.Object);
            var result = shipment.GetShipment();

            //Assert

            Assert.AreEqual(result, "1234");
        }

    }
}

