using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreatePedidoItem
{
    public interface ICreatePedidoItemUseCase
    {
        PeiPedidoIten CriarPedidoItem(ProProduto produto, PriPrice preco, long quantidade, long subtotal)
    }
}
