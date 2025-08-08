using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.Pedido.GetTotalValorPedido
{
    public class GetTotalValorPedidoUseCase : IGetTotalValorPedidoUseCase
    {
        public decimal CalculateTotalValorPedido(List<OriOrderItem> listaPedido)
        {
            return listaPedido.Sum(item => item.OriIntValorUnit * item.OriIntQuantity);
        }
    }
}
