using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.Services.ExternalServices.Contracts.Stripe;
public interface IWebHookHandler
{
    Task HandleEventAsync(string json, string stripeSignature);
}
