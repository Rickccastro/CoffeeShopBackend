using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreatePedidoItem
{
    public class CreatePedidoItemUseCase : ICreatePedidoItemUseCase
    {
        public PeiPedidoIten CriarPedidoItem(ProProduto produto, PriPrice preco, long quantidade, long subtotal)
        {
            return new PeiPedidoIten
            {
                PeiIdPedidoItens = Guid.NewGuid(),
                PeiIdProduto = produto.ProIdProduto,
                PeiIdPreco = preco.PriId,
                PeiIntValorUnit = preco.PriPrecoUnitario,
                PeiIntQuantidade = quantidade,
                PeiIntValorTotal = subtotal
            };
        }
    }
}
