using CakeCompany.Provider.Factory;

namespace CakeCompany.Models.Transport;

internal class Ship: ITransport
{
    public bool Deliver(List<Product> products)
    {
        return true;
    }
}