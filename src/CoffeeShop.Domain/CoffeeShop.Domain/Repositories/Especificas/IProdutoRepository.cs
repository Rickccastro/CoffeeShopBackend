using CoffeeShop.Domain.Entities;


namespace CoffeeShop.Domain.Repositories.Especificas
{
    public interface IProdutoRepository : IRepositoryBase<ProProduto>
    {
        Task<List<ProProduto>> ObterListaIdsProdutosAsync(List<string> listaIdsProduto);
    }
}
