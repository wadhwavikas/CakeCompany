using System.Security.Cryptography;
using CakeCompany.Models;

namespace CakeCompany.Provider
{
    public interface ITransportProvider
    {
        string CheckForAvailability(List<Product> products);
        string ProcessTranportation(List<Product> products);
    }
}