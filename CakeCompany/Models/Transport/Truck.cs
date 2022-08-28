using CakeCompany.Provider.Factory;

namespace CakeCompany.Models.Transport;

internal class Truck : ITransport
{
    public bool Deliver(List<Product> products)
    {
        return true;
    }
}