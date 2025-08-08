using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Application.UseCase.Produto.GetById
{
    public class GetListaProdutoByIdsUseCase : IGetListaProdutoByIdsUseCase
    {
        private readonly IProductRepository _produtoRepository;

        public GetListaProdutoByIdsUseCase(IProductRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProProduct>> GetListaProdutosAsync(List<CheckoutListItemRequest> items)
        {
            var listaIdsProduto = items.Select(x => x.ProdutoId).Distinct().ToList();

            var produtos = await _produtoRepository.ObterListaIdsProdutosAsync(listaIdsProduto);

            // Validação: garantir que todos os produtos foram encontrados
            if (produtos.Count != listaIdsProduto.Count)
            {
                var encontrados = produtos.Select(p => p.ProIdProduct);
                var faltantes = listaIdsProduto.Except(encontrados);
                throw new InvalidOperationException($"Produtos não encontrados: {string.Join(", ", faltantes)}");
            }

            return produtos;
        }
    }
}
