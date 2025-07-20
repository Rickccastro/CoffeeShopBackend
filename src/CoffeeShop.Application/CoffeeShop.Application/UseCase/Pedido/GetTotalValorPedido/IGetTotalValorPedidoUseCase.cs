using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Pedido.GetTotalValorPedido
{
    public interface IGetTotalValorPedidoUseCase
    {
        decimal CalculateTotalValorPedido(List<PeiPedidoIten> listaPedido);
    }
}
