using System;
using CakeCompany.Models.Transport;

namespace CakeCompany.Provider.Factory
{
    public class FactoryTransport
    {
        public FactoryTransport()
        {

        }

        static public ITransport GetTransport(string? transportType)
        {
            ITransport transport;

            if (transportType == "Van")
                transport = new Van();

            else if (transportType == "Truck")
                transport = new Truck();

            else
                transport = new Ship();

            return transport;
        }
    }
}

