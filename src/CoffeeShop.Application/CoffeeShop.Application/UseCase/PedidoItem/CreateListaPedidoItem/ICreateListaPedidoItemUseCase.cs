using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreateListaPedidoItem
{
    public interface ICreateListaPedidoItemUseCase
    {
        Task<List<PeiPedidoIten>> CreateListaPedidoItem(List<CheckoutListItemRequest> itens);
    }
}
