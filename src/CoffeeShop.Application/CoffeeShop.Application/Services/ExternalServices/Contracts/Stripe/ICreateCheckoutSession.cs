using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.Services.ExternalServices.Contracts.Stripe;

public interface ICreateCheckoutSession
{
    CheckoutSessionResult CreateSession(List<OriOrderItem> lineItems);
}
