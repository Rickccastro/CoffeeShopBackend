using CoffeeShop.Application.Services.ExternalServices.Contracts.Stripe;
using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Application.Services.InternalServices;
using CoffeeShop.Application.UseCase.Pedido.Create;
using CoffeeShop.Application.UseCase.PedidoItem.CreateListaPedidoItem;
using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Application.Orchestrator.Checkout.Create
{
    public class CreateCheckoutOrchestrator : ICreateCheckoutOrchestrator
    {
        private readonly ICreateCheckoutSession  _createCheckoutSession;
        private readonly ICreateListaPedidoItemUseCase _createListaPedidoItemUseCase;
        private readonly ICreatePedidoUseCase _createPedidoUseCase;
        private readonly ILoggedUser _loggedUser;

        public CreateCheckoutOrchestrator(
             IOrderRepository pedidoRepository,
             ICreateCheckoutSession createCheckoutSession,
             ICreateListaPedidoItemUseCase createListaPedidoItemUseCase,
             ICreatePedidoUseCase createPedidoUseCase,
             ILoggedUser loggedUser)
        {
            _createCheckoutSession = createCheckoutSession;
            _createListaPedidoItemUseCase = createListaPedidoItemUseCase;
            _createPedidoUseCase = createPedidoUseCase;
            _loggedUser = loggedUser;
        }

        public async Task<CheckoutSessionResult> CreateCheckout(CheckoutRequest request)
        {
            var loggedUser = await _loggedUser.Get();

            var listaPedidoItem = await _createListaPedidoItemUseCase.CreateListaPedidoItem(request.Items);

            var session = _createCheckoutSession.CreateSession(listaPedidoItem);

            await _createPedidoUseCase.CreatePedido(loggedUser.UsrIdUser, listaPedidoItem, session);

            return session;
        }
    }
}
