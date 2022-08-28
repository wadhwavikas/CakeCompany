using CakeCompany.Models;

namespace CakeCompany.Provider
{
    public interface ICakeProvider
    {
        Product Bake(Order order);
        DateTime Check(Order order);
    }
}