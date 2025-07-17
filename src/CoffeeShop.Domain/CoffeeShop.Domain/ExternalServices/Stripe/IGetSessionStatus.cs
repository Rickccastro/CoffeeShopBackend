using CoffeeShop.Domain.ExternalServices.Stripe.Entities;

namespace CoffeeShop.Domain.ExternalServices.Stripe
{
    public interface IGetSessionStatus
    {
        SessionStatus GetStatus(string sessionId);
    }
}
