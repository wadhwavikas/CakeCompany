using CakeCompany.Models;

namespace CakeCompany.Provider
{
    public interface IPaymentProvider
    {
        PaymentIn Process(Order order);
    }
}