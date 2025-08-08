using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Application.UseCase.Pedido.GetTotalValorPedido;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Application.UseCase.Pedido.Create
{
    public class CreatePedidoUseCase : ICreatePedidoUseCase
    {
        private readonly IOrderRepository _pedidoRepository;
        private readonly IGetTotalValorPedidoUseCase _getTotalValorPedidoUseCase;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePedidoUseCase(
             IProductRepository produtoRepository, IPriceRepository precoRepository,
             IOrderRepository pedidoRepository, IUnitOfWork unitOfWork,
             IGetTotalValorPedidoUseCase getTotalValorPedidoUseCase)
        {
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
            _getTotalValorPedidoUseCase = getTotalValorPedidoUseCase;
        }


        public async Task <OrdOrder> CreatePedido(Guid userId, List<OriOrderItem> orderItems, CheckoutSessionResult session)
        {
            var orderId = Guid.NewGuid();
            var paymentId = Guid.NewGuid();

            var order = new OrdOrder
            {
                OrdIdOrder = orderId,
                OrdUsrId = userId,
                OrdEnumStatusOrder = OrderStatus.AWAITING_PAYMENT.ToValue(),
                OrdIntTotalCostOrder = Convert.ToInt32(_getTotalValorPedidoUseCase.CalculateTotalValorPedido(orderItems)),
                OrdDateCreated = DateTime.UtcNow,
                PayPayments = new PayPayment
                {
                    PayIdPayment = session.SessionId,
                    PayIdPaymentIntent = session.PaymentIntentId,
                    PayDateCreated = DateTime.UtcNow,
                    PayEnumStatus = session.Status.ToString().ToUpper(),
                    PayNmReceiptUrl =  string.Empty,
                    PayIntAmountTotal = Convert.ToInt32(_getTotalValorPedidoUseCase.CalculateTotalValorPedido(orderItems)),
                    PayEnumRefundedStatus = PaymentRefunds.NONE.ToValue(),
                    PayOrderId = orderId,
                },
                OriOrderItems = orderItems, 
            };

                _pedidoRepository.AttachProdutoAndPrice(orderItems);
                await _pedidoRepository.AdicionarAsync(order);

                await _unitOfWork.Commit();
            return order;
        }
    }
}
