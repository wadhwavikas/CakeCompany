using System;
using CakeCompany.Models;

namespace CakeCompany.Provider.Factory
{
    public interface ITransport
    {
        bool Deliver( List<Product> products);
    }
}

