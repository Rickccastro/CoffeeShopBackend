using CoffeeShop.Application.ExternalServices.Contracts.Stripe;
using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Application.UseCase.Pedido.Create;
using CoffeeShop.Application.UseCase.PedidoItem.CreateListaPedidoItem;
using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Application.UseCase.Checkout.Create
{
    public class CreateCheckoutUseCase : ICreateCheckoutUseCase
    {
        private readonly ICreateCheckoutSession  _createCheckoutSession;
        private readonly ICreateListaPedidoItemUseCase _createListaPedidoItemUseCase;
        private readonly ICreatePedidoUseCase _createPedidoUseCase;

        public CreateCheckoutUseCase(
             IPedidoRepository pedidoRepository,
             ICreateCheckoutSession createCheckoutSession,
             ICreateListaPedidoItemUseCase createListaPedidoItemUseCase,
             ICreatePedidoUseCase createPedidoUseCase)
        {
            _createCheckoutSession = createCheckoutSession;
            _createListaPedidoItemUseCase = createListaPedidoItemUseCase;
            _createPedidoUseCase = createPedidoUseCase;
        }

        public async Task<CheckoutSessionResult> CreateCheckout(CheckoutRequest request)
        {
            var listaPedidoItem = await _createListaPedidoItemUseCase.CreateListaPedidoItem(request.Items);

            var session = _createCheckoutSession.CreateSession(listaPedidoItem);

            await _createPedidoUseCase.CreatePedido(Guid.Parse(request.UserId), listaPedidoItem, session);

            return session;
        }
    }
}
