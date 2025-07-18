using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;

namespace CoffeeShop.Application.UseCase.Pedido.Create
{
    public interface ICreatePedidoUseCase
    {
       Task <PedPedido> CreatePedido(Guid usuarioId, List<PeiPedidoIten> pedidoItens, CheckoutSessionResult session);
    }
}
