namespace CoffeeShop.Application.ExternalServices.Contracts.Stripe;
public interface IExpireCheckoutSession
{
   public void ExpireCheckout(string sessionId);  
}
