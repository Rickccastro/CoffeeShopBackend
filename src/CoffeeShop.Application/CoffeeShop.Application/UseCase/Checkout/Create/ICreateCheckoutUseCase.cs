using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Communication.Requests.Checkout;

namespace CoffeeShop.Application.UseCase.Checkout.Create
{
    public interface ICreateCheckoutUseCase
    {
      Task<CheckoutSessionResult> CreateCheckout(CheckoutRequest request);
    }
}
