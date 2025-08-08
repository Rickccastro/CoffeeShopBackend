using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreatePedidoItem
{
    public interface ICreatePedidoItemUseCase
    {
        OriOrderItem CriarPedidoItem(CheckoutItemRequest checkoutItem);
    }
}
