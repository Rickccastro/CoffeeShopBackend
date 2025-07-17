using CoffeeShop.Domain.ExternalServices.Stripe.Entities;

namespace CoffeeShop.Domain.ExternalServices.Stripe
{
    public interface ICreateCheckoutSession
    {
        CheckoutSessionResult CreateSession(List<CheckoutItemRequest> lineItems);
    }
}
