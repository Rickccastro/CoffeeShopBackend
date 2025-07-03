using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Communication.Responses;
using CoffeeShop.Domain.Repositories;
using Stripe.Checkout;
using System.Threading.Tasks;



namespace CoffeeShop.Application.UseCase.Checkout.Create
{
    public class CreateCheckoutUseCase : ICreateCheckoutUseCase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPrecoRepository _precoRepository;

        public CreateCheckoutUseCase(IProdutoRepository produtoRepository, IPrecoRepository precoRepository)
        {
            _produtoRepository = produtoRepository;
            _precoRepository = precoRepository;
        }

        public async Task<Session> CreateCheckout(CheckoutRequest request)
        {
            var domain = "http://localhost:4200";
            var lineItems = new List<SessionLineItemOptions>();

            foreach (var item in request.Items)
            {
                var produto = await _produtoRepository.ObterPorIdAsync(item.ProdutoId)
                              ?? throw new Exception("Produto não encontrado");

                var preco = await  _precoRepository.ObterPrecoVigenteAsync(item.ProdutoId, DateTime.UtcNow)
                             ?? throw new Exception("Preço não encontrado ou expirado");

                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "brl",
                        UnitAmount = preco.PriPrecoUnitario,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = produto.CafNmTitle,
                            Description = produto.CafNmSubtitle,
                            Images = new List<string> { produto.ProNmImgSrc }
                        },
                    },
                    Quantity = item.Quantity,
                });
            }

            var options = new SessionCreateOptions
            {
                UiMode = "embedded",
                LineItems = lineItems,
                Mode = "payment",
                ReturnUrl = domain + "/return-checkout?session_id={CHECKOUT_SESSION_ID}",
            };

            var service = new SessionService();
            return service.Create(options);
        }
    }
}
