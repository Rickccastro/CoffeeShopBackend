using CoffeeShop.Application.UseCase.PedidoItem.CreatePedidoItem;
using CoffeeShop.Application.UseCase.Preco.GetPrecoVigente;
using CoffeeShop.Application.UseCase.Produto.GetById;
using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;

namespace CoffeeShop.Application.UseCase.Checkout.CreateCheckoutLineItems
{
    public class CreateCheckoutLineItemsUseCase : ICreateCheckoutLineItemsUseCase
    {
        private readonly IGetListaProdutoByIdsUseCase _getListaProdutoByIdsUseCase;
        private readonly ICreatePedidoItemUseCase _createPedidoItemUseCase;
        private readonly IGetPrecoVigenteUseCase _getPrecoVigenteUseCase;

        public CreateCheckoutLineItemsUseCase(
            IGetListaProdutoByIdsUseCase getListaProdutoByIdsUseCase,
            ICreatePedidoItemUseCase createPedidoItemUseCase,
            IGetPrecoVigenteUseCase getPrecoVigenteUseCase)
        {
            _getListaProdutoByIdsUseCase = getListaProdutoByIdsUseCase;
            _createPedidoItemUseCase = createPedidoItemUseCase;
            _getPrecoVigenteUseCase = getPrecoVigenteUseCase;
        }

        public async Task <List<CheckoutItemRequest>> ProcessarItensAsync (List<CheckoutListItemRequest> itens, DateTime dataAtual)
        {
            var lineItems = new List<CheckoutItemRequest>();

            var produtos = await _getListaProdutoByIdsUseCase.GetListaProdutosAsync(itens);

            foreach (var item in itens)
            {
                var produto = produtos.FirstOrDefault(p => p.ProIdProduto == item.ProdutoId)
                              ?? throw new InvalidOperationException($"Produto {item.ProdutoId} não encontrado.");

                var preco = _getPrecoVigenteUseCase.GetPrecoVigente(produto, dataAtual);

                lineItems.Add(new CheckoutItemRequest
                {
                    PrecoId = preco.PriId,
                    ProdutoId = produto.ProIdProduto,
                    PrecoUnitario = preco.PriPrecoUnitario,
                    ValorTotalItem = preco.PriPrecoUnitario * item.Quantity,
                    ProdutoDescricao = produto.ProNmSubtitle,
                    ProdutoImagem = produto.ProNmImgSrc,
                    ProdutoNome = produto.ProNmTitle,
                    Quantidade = item.Quantity
                });
            }

            return lineItems;
        }
    }
}
