using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;

namespace CoffeeShop.Application.UseCase.Checkout.Create
{
    public interface ICreateCheckoutUseCase
    {
      Task<CheckoutSessionResult> CreateCheckout(CheckoutRequest request);
    }
}
