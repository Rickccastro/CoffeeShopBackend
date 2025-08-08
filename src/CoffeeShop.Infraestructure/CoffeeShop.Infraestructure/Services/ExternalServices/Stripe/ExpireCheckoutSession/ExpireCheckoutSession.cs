using CoffeeShop.Application.Services.ExternalServices.Contracts.Stripe;
using Stripe.Checkout;

namespace CoffeeShop.Infraestructure.Services.ExternalServices.Stripe.ExpireCheckoutSession;
public class ExpireCheckoutSession : IExpireCheckoutSession
{
   public void ExpireCheckout(string sessionId)
    {

        var service = new SessionService().Expire(sessionId);
    }
}
