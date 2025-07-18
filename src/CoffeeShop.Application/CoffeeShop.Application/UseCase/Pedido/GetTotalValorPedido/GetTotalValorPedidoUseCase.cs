using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Pedido.GetTotalValorPedido
{
    public class GetTotalValorPedidoUseCase : IGetTotalValorPedidoUseCase
    {
        public long CalculateTotalValorPedido(List<PeiPedidoIten> listaPedido)
        {
            return listaPedido.Sum(item => item.PeiIntValorUnit * item.PeiIntQuantidade);
        }
    }
}
