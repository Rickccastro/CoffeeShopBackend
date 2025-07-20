using CoffeeShop.Application.ExternalServices.Contracts.Stripe;
using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Application.UseCase.Checkout.CreateCheckoutLineItems;
using CoffeeShop.Application.UseCase.Pedido.Create;
using CoffeeShop.Application.UseCase.PedidoItem.CreateListaPedidoItem;
using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Application.UseCase.Checkout.Create
{
    public class CreateCheckoutUseCase : ICreateCheckoutUseCase
    {
        private readonly ICreateCheckoutSession  _createCheckoutSession;
        private readonly ICreateListaPedidoItemUseCase _createListaPedidoItemUseCase;
        private readonly ICreateCheckoutLineItemsUseCase _createCheckoutLineItemsUseCase;
        private readonly ICreatePedidoUseCase _createPedidoUseCase;

        public CreateCheckoutUseCase(
             IPedidoRepository pedidoRepository,
             ICreateCheckoutSession createCheckoutSession,
             ICreateListaPedidoItemUseCase createListaPedidoItemUseCase,
             ICreateCheckoutLineItemsUseCase createCheckoutLineItemsUseCase,
             ICreatePedidoUseCase createPedidoUseCase)
        {
            _createCheckoutSession = createCheckoutSession;
            _createListaPedidoItemUseCase = createListaPedidoItemUseCase;
            _createPedidoUseCase = createPedidoUseCase;
            _createCheckoutLineItemsUseCase = createCheckoutLineItemsUseCase;
        }

        public async Task<CheckoutSessionResult> CreateCheckout(CheckoutRequest request)
        {
            var dataAtual = DateTime.UtcNow;  
            
            var listaPedidoItem = await _createCheckoutLineItemsUseCase.ProcessarItensAsync(request.Items, dataAtual);
            //var listaPedidoItem = await _createListaPedidoItemUseCase.CreateListaPedidoItem(listaLineItems, dataAtual);

            var session = _createCheckoutSession.CreateSession(listaPedidoItem);

            await _createPedidoUseCase.CreatePedido(Guid.Parse(request.UserId), listaPedidoItem, session);

            return session;
        }
    }
}
