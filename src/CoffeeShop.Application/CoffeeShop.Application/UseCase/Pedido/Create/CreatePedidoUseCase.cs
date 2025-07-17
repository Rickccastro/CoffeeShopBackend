using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Application.UseCase.Pedido.Create
{
    public class CreatePedidoUseCase : ICreatePedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePedidoUseCase(
             IProdutoRepository produtoRepository, IPrecoRepository precoRepository,
             IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork)
        {
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task <PedPedido> CreatePedido(Guid usuarioId, List<PeiPedidoIten> pedidoItens, long valorTotal, CheckoutSessionResult session)
        {
            var pedidoId = Guid.NewGuid();  

            var pedido = new PedPedido
            {
                PedIdPedido = pedidoId,
                PedUsrId = usuarioId,
                PedEnumStatusPedido = PedidoStatus.Pendente.ToString().ToUpper(),
                PedIntValorTotal = valorTotal,
                PedDateCriacao = DateOnly.FromDateTime(DateTime.UtcNow),
                PedStripeSessionId = session.SessionId,
                PedStripePaymentIntentId = session.PaymentIntentId,
                PeiPedidoItens = pedidoItens,             
                StrStripeSessaos = new List<StrStripeSessao>
        {
            new StrStripeSessao
            {
                StrIdStripeSessao = Guid.NewGuid(),
                StrIdPedido = pedidoId,
                StrIdSession = session.SessionId,
                StrNmPaymentIntentId = session.PaymentIntentId,
                StrNmStatus = session.Status,
                StrEnumModo = "payment",
            }
        }
            };

            await _pedidoRepository.AdicionarAsync(pedido);

            await _unitOfWork.Commit();
            return pedido;
        }
    }
}
