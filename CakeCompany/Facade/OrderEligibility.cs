using System;
using CakeCompany.Provider;
using CakeCompany.Models;

namespace CakeCompany.Facade
{
    public class OrderEligibility
    {
        private ICakeProvider cakeProvider;
        private IPaymentProvider paymentProvider;
        public OrderEligibility()
        {
        }

        public OrderEligibility(ICakeProvider cakeProvider, IPaymentProvider paymentProvider)
        {
            this.cakeProvider = cakeProvider;
            this.paymentProvider = paymentProvider;
        }

        public bool isEligible(Models.Order order)
        {
            if (cakeProvider == null)
                cakeProvider = new CakeProvider();

            if (paymentProvider == null)
                paymentProvider = new PaymentProvider();

            var estimatedBakeTime = cakeProvider.Check(order);

            if (estimatedBakeTime > order.EstimatedDeliveryTime)
            {
                return false;
            }

            if (!paymentProvider.Process(order).IsSuccessful)
            {
                return false;
            }

            return true;
        }



    }
}

