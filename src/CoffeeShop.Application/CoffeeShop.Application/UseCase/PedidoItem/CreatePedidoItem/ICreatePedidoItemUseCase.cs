using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreatePedidoItem
{
    public interface ICreatePedidoItemUseCase
    {
        PeiPedidoIten CriarPedidoItem(CheckoutItemRequest checkoutItem);
    }
}
