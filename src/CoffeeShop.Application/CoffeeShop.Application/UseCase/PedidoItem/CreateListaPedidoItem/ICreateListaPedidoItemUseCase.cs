using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreateListaPedidoItem
{
    public interface ICreateListaPedidoItemUseCase
    {
       Task<(List<PeiPedidoIten> PedidoItens, List<CheckoutItemRequest> LineItems, long ValorTotal)> 
            ProcessarItensAsync(List<CheckoutListItemRequest> itens, DateTime dataAtual);
    }
}
