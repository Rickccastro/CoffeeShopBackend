using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreatePedidoItem
{
    public class CreatePedidoItemUseCase : ICreatePedidoItemUseCase
    {
        public PeiPedidoIten CriarPedidoItem(CheckoutItemRequest checkoutItem)
        {
            return new PeiPedidoIten
            {
                PeiIdPedidoItens = Guid.NewGuid(),
                PeiIdProduto = checkoutItem.Produto.ProIdProduto,
                PeiIdPreco = checkoutItem.Preco.PriId,
                PeiIntValorUnit = checkoutItem.Preco.PriPrecoUnitario,
                PeiIntQuantidade = checkoutItem.Quantidade,
                PeiIntValorTotal = checkoutItem.ValorTotalItem
            };
        }
    }
}
