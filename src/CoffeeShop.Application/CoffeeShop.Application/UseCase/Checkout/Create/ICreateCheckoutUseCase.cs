using CoffeeShop.Communication.Requests.Checkout;
using Stripe.Checkout;

namespace CoffeeShop.Application.UseCase.Checkout.Create
{
    public interface ICreateCheckoutUseCase
    {
       Session CreateCheckout(CheckoutRequest request);
    }
}
