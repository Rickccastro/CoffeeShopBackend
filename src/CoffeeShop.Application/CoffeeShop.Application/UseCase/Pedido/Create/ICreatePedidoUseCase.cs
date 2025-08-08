using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Pedido.Create
{
    public interface ICreatePedidoUseCase
    {
       Task <OrdOrder> CreatePedido(Guid usuarioId, List<OriOrderItem> pedidoItens, CheckoutSessionResult session);
    }
}
