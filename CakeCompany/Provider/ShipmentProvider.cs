using System.Diagnostics;
using System.Reflection;
using CakeCompany.Models;

namespace CakeCompany.Provider;

public class ShipmentProvider : IShipmentProvider
{
    private ITransportProvider transportProvider;
    private IOrderProvider orderProvider;


    public ShipmentProvider()
    {
    }

    public ShipmentProvider(IOrderProvider orderProvider, ITransportProvider transportProvider)
    {
        this.transportProvider = transportProvider;
        this.orderProvider = orderProvider;
    }

    public string GetShipment()
    {
        try
        {
            //Call order to get new orders

            if (orderProvider == null)
                orderProvider = new OrderProvider();

            var orders = orderProvider.GetLatestOrders();

            if (!orders.Any())
            {
                return String.Empty;
            }

            var products = orderProvider.ProcessOrder(orders);

            return ShipProducts(products);
        }
        catch (Exception ex)
        {
            ILogProvider logger = new LogProvider();
            logger.Log(MethodBase.GetCurrentMethod()?.Name + ex.Message);

            return String.Empty;
        }
    }

    private string ShipProducts(List<Product> products)
    {
        if (transportProvider == null)
            transportProvider = new TransportProvider();
        string shipmentNumber = transportProvider.ProcessTranportation(products);
        return shipmentNumber;
    }

}
