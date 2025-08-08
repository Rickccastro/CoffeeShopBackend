using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreatePedidoItem
{
    public class CreatePedidoItemUseCase : ICreatePedidoItemUseCase
    {
        public OriOrderItem CriarPedidoItem(CheckoutItemRequest checkoutItem)
        {
            return new OriOrderItem
            {
                OriIdItemsOrder = Guid.NewGuid(),
                OriIdProduct = checkoutItem.Produto.ProIdProduct,
                OriIdPrice = checkoutItem.Preco.PriIdPrice,
                OriIntValorUnit = checkoutItem.Preco.PriIntUnitPrice,
                OriIntQuantity = checkoutItem.Quantidade,
                OriIntTotalValueItem = checkoutItem.ValorTotalItem
            };
        }
    }
}
