using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Pedido.Create
{
    public interface ICreatePedidoUseCase
    {
       Task <PedPedido> CreatePedido(Guid usuarioId, List<PeiPedidoIten> pedidoItens, CheckoutSessionResult session);
    }
}
