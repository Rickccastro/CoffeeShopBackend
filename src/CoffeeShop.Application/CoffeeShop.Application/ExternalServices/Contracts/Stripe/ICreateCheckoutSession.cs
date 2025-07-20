using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.ExternalServices.Contracts.Stripe;

public interface ICreateCheckoutSession
{
    CheckoutSessionResult CreateSession(List<PeiPedidoIten> lineItems);
}
