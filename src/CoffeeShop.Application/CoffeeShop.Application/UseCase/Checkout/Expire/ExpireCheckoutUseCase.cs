using CoffeeShop.Application.ExternalServices.Contracts.AWS;
using CoffeeShop.Application.ExternalServices.Contracts.Stripe;

namespace CoffeeShop.Application.UseCase.Checkout.Expire;
internal class ExpireCheckoutUseCase : IExpireCheckoutUseCase
{
    private readonly IExpireCheckoutSession _expireCheckoutSession;
    public ExpireCheckoutUseCase(IExpireCheckoutSession expireCheckoutSession)
    {
        _expireCheckoutSession = expireCheckoutSession;
    }
    public void ExpireCheckout(string sessionId)
    {
       _expireCheckoutSession.ExpireCheckout(sessionId); 
    }
}
