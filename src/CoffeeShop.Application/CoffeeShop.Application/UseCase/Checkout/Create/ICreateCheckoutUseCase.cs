using CoffeeShop.Communication.Requests;
using Stripe.Checkout;

namespace CoffeeShop.Application.UseCase.Checkout.Create
{
    public interface ICreateCheckoutUseCase
    {
       Session CreateCheckout(CheckoutRequest request);
    }
}
