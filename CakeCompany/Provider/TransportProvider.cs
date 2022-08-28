using CakeCompany.Models;
using CakeCompany.Provider.Factory;

namespace CakeCompany.Provider;

internal class TransportProvider : ITransportProvider
{
    public string CheckForAvailability(List<Product> products)
    {
        if (products.Sum(p => p.Quantity) < 1000)
        {
            return "Van";
        }

        if (products.Sum(p => p.Quantity) > 1000 && products.Sum(p => p.Quantity) < 5000)
        {
            return "Truck";
        }

        return "Ship";
    }

    public string ProcessTranportation(List<Product> products)
    {
        var transport = CheckForAvailability(products);

        ITransport objectTransport;
        objectTransport = FactoryTransport.GetTransport(transport);
        objectTransport.Deliver(products);
        Random random = new Random();
        return random.Next(10000).ToString();
    }

}
