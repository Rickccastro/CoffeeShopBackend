using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreateListaPedidoItem
{
    public interface ICreateListaPedidoItemUseCase
    {
        Task<List<OriOrderItem>> CreateListaPedidoItem(List<CheckoutListItemRequest> itens);
    }
}
