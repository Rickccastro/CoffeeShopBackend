using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreateListaPedidoItem
{
    public interface ICreateListaPedidoItemUseCase
    {
        Task<List<PeiPedidoIten>> CreateListaPedidoItem(List<CheckoutItemRequest> LineItems, DateTime dataAtual);
    }
}
