using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Pedido.GetTotalValorPedido
{
    public interface IGetTotalValorPedidoUseCase
    {
        long CalculateTotalValorPedido(List<PeiPedidoIten> listaPedido);
    }
}
