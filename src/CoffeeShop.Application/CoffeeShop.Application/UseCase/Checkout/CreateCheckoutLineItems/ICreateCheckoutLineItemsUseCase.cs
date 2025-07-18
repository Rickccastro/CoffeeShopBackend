using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;

namespace CoffeeShop.Application.UseCase.Checkout.CreateCheckoutLineItems
{
    public interface ICreateCheckoutLineItemsUseCase
    {
        Task<List<CheckoutItemRequest>> ProcessarItensAsync(List<CheckoutListItemRequest> itens, DateTime dataAtual);
    }
}
