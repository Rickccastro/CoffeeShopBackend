using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.Repositories.Especificas;
using Stripe.Checkout;
using Stripe.Forwarding;



namespace CoffeeShop.Application.UseCase.Checkout.Create
{
    public class CreateCheckoutUseCase : ICreateCheckoutUseCase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPrecoRepository _precoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCheckoutUseCase(
             IProdutoRepository produtoRepository, IPrecoRepository precoRepository,
             IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork)
        {
            _produtoRepository = produtoRepository;
            _precoRepository = precoRepository;
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
        }

        public void Conversoes()
        {
        }

        public async Task<Session> CreateCheckout(CheckoutRequest request)
        {
            var pedidoId = Guid.NewGuid();
            var dataAtual = DateTime.UtcNow;

            var itensProcessados = await ProcessarItensAsync(request.Items, pedidoId, dataAtual);

            var session = CreateSession(itensProcessados.LineItems);

            var pedido = CriarPedido(pedidoId, Guid.Parse(request.UserId) , itensProcessados.PedidoItens,
                itensProcessados.ValorTotal, session);

            await _pedidoRepository.AdicionarAsync(pedido);

            await _unitOfWork.Commit();

            return session;
        }


        private async Task<(List<PeiPedidoIten> PedidoItens, List<SessionLineItemOptions> LineItems, long ValorTotal)>
    ProcessarItensAsync(List<CheckoutListItemRequest> itens, Guid pedidoId, DateTime data)
        {
            var pedidoItens = new List<PeiPedidoIten>();
            var lineItems = new List<SessionLineItemOptions>();
            long valorTotal = 0;

            foreach (var item in itens)
            {
                var produto = await _produtoRepository.ObterPorIdStringAsync(item.ProdutoId) ?? throw new InvalidOperationException($"Produto com ID {item.ProdutoId} não encontrado.");

                var preco = await _precoRepository.ObterPrecoVigenteAsync(produto.ProIdProduto, data)
                              ?? throw new InvalidOperationException($"Preço vigente para produto {item.ProdutoId} não encontrado.");

                long subtotal = preco.PriPrecoUnitario * item.Quantity;
                valorTotal += subtotal;

                pedidoItens.Add(new PeiPedidoIten
                {
                    PeiIdPedidoItens = Guid.NewGuid(),
                    PeiIdPedido = pedidoId,
                    PeiIdProduto = produto.ProIdProduto,
                    PeiIdPreco = preco.PriId,
                    PeiIntValorUnit = preco.PriPrecoUnitario,
                    PeiIntQuantidade = item.Quantity,
                    PeiIntValorTotal = subtotal
                });

                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "brl",
                        UnitAmount = preco.PriPrecoUnitario,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = produto.ProNmTitle,
                            Description = produto.ProNmSubtitle,
                            Images = new List<string> { produto.ProNmImgSrc }
                        },
                    },
                    Quantity = item.Quantity,
                });
            }

            return (pedidoItens, lineItems, valorTotal);
        }

        private PedPedido CriarPedido(Guid pedidoId, Guid usuarioId, List<PeiPedidoIten> pedidoItens, long valorTotal, Session session)
        {
            return new PedPedido
            {
                PedIdPedido = pedidoId,
                PedUsrId = usuarioId,
                PedEnumStatusPedido = PedidoStatus.Pendente.ToString().ToUpper(),
                PedIntValorTotal = valorTotal,
                PedDateCriacao = DateOnly.FromDateTime(DateTime.UtcNow),
                PedStripeSessionId = session.Id,
                PedStripePaymentIntentId = session.PaymentIntentId,
                PeiPedidoItens = pedidoItens,
                StrStripeSessaos = new List<StrStripeSessao>
        {
            new StrStripeSessao
            {
                StrIdStripeSessao = Guid.NewGuid(),
                StrIdPedido = pedidoId,
                StrIdSession = session.Id,
                StrNmPaymentIntentId = session.PaymentIntentId,
                StrNmStatus = session.Status,
                StrEnumModo = session.Mode
            }
        }
            };
        }
        public Session CreateSession(List<SessionLineItemOptions> lineItems)
        {
            var domain = "http://localhost:4200";
            var sessionOptions = new SessionCreateOptions
            {
                UiMode = "embedded",
                LineItems = lineItems,
                Mode = "payment",
                ReturnUrl = domain + "/return-checkout?session_id={CHECKOUT_SESSION_ID}",
            };

            var service = new SessionService();
            return service.Create(sessionOptions);
        }
    }
}
