using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Communication.Requests.Checkout;

namespace CoffeeShop.Application.Orchestrator.Checkout.Create
{
    public interface ICreateCheckoutOrchestrator
    {
      Task<CheckoutSessionResult> CreateCheckout(CheckoutRequest request);
    }
}
