using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Checkout.CreateCheckoutLineItems
{
    public interface ICreateCheckoutLineItemsUseCase
    {
        Task<List<PeiPedidoIten>> ProcessarItensAsync(List<CheckoutListItemRequest> itens, DateTime dataAtual);
    }
}
