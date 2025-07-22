using CoffeeShop.Application.ExternalServices.Contracts.Stripe;
using Stripe.Checkout;

namespace CoffeeShop.Infraestructure.ExternalServices.Stripe.ExpireCheckoutSession;
public class ExpireCheckoutSession : IExpireCheckoutSession
{
   public void ExpireCheckout(string sessionId)
    {

        var service = new SessionService().Expire(sessionId);
    }
}
