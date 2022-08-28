using System.Reflection.Metadata.Ecma335;
using CakeCompany.Facade;
using CakeCompany.Models;
using System.Linq;

namespace CakeCompany.Provider;

public class OrderProvider : IOrderProvider
{
    private ICakeProvider cakeProvider;
    
    public OrderProvider(ICakeProvider cakeProvider)
    {
        this.cakeProvider = cakeProvider;
    }

    public OrderProvider()
    {
    }

    public Models.Order[] GetLatestOrders()
    {
        return new Models.Order[]
        {
            new("CakeBox", DateTime.Now, 1, Cake.RedVelvet, 120.25),
            new("ImportantCakeShop", DateTime.Now, 1, Cake.RedVelvet, 120.25),
            new("AdditionCake", DateTime.Now.AddHours(2), 1, Cake.RedVelvet, 120.25)
        };
    }

    public List<Product> ProcessOrder(Models.Order[] orders)
    {
        List<Models.Order> cancelledOrders = GetCancelledOrders(orders);

        var eligibleOrder = orders.Except(cancelledOrders).ToList();

        var products = new List<Product>();
         cakeProvider=new CakeProvider();

        products.AddRange(from order in eligibleOrder
                          let product = cakeProvider.Bake(order)
                          select product);


        return products;
    }

    public List<Models.Order> GetCancelledOrders(Models.Order[] orders)
    {
        OrderEligibility ckeckOrder = new OrderEligibility();
        var cancelledOrders = new List<Models.Order>();

        cancelledOrders = orders.Where(order => !ckeckOrder.isEligible(order)).ToList();
        return cancelledOrders;
    }

    public void UpdateOrders(Models.Order[] orders)
    {
    }

    
}


