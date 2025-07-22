namespace CoffeeShop.Application.UseCase.Checkout.Expire;
public interface IExpireCheckoutUseCase
{
    public void ExpireCheckout(string sessionId);
}
