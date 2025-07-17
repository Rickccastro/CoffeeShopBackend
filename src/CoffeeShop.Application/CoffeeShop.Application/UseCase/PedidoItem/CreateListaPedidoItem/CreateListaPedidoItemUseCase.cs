using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Application.UseCase.PedidoItem.CreateListaPedidoItem
{
    public class CreateListaPedidoItemUseCase : ICreateListaPedidoItemUseCase
    {
        private readonly IProdutoRepository _produtoRepository;
        public CreateListaPedidoItemUseCase(
             IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }


        public async Task<(List<PeiPedidoIten> PedidoItens, List<CheckoutItemRequest> LineItems, long ValorTotal)>
 ProcessarItensAsync(List<CheckoutListItemRequest> itens, DateTime dataAtual)
        {
            var pedidoItens = new List<PeiPedidoIten>();
            var lineItems = new List<CheckoutItemRequest>();
            long valorTotal = 0;


            foreach (var item in itens)
            {
                var produto = await _produtoRepository.ObterPorIdStringAsync(item.ProdutoId) ?? throw new InvalidOperationException($"Produto com ID {item.ProdutoId} não encontrado.");

                var preco = produto.PriPrices
                         .FirstOrDefault(p => p.PriDataInicio <= dataAtual && (p.PriDataFim == null || p.PriDataFim >= dataAtual))
                         ?? throw new InvalidOperationException($"Preço vigente para produto {item.ProdutoId} não encontrado.");

                long subtotal = preco.PriPrecoUnitario * item.Quantity;

                valorTotal += subtotal;

                pedidoItens.Add(new PeiPedidoIten
                {
                    PeiIdPedidoItens = Guid.NewGuid(),
                    PeiIdProduto = produto.ProIdProduto,
                    PeiIdPreco = preco.PriId,
                    PeiIntValorUnit = preco.PriPrecoUnitario,
                    PeiIntQuantidade = item.Quantity,
                    PeiIntValorTotal = subtotal
                });

                lineItems.Add(new CheckoutItemRequest
                {
                    PrecoId = preco.PriId,
                    ProdutoId = produto.ProIdProduto,
                    PrecoUnitario = preco.PriPrecoUnitario,
                    ProdutoDescricao = produto.ProNmSubtitle,
                    ProdutoImagem = produto.ProNmImgSrc,
                    ProdutoNome = produto.ProNmTitle,
                    Quantidade = item.Quantity
                });
            }

            return (pedidoItens, lineItems, valorTotal);
        }
    }
}
