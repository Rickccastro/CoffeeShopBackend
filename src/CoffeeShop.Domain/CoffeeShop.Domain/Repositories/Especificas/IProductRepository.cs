using CoffeeShop.Domain.Entities;


namespace CoffeeShop.Domain.Repositories.Especificas
{
    public interface IProductRepository : IRepositoryBase<ProProduct>
    {
        Task<List<ProProduct>> ObterListaIdsProdutosAsync(List<string> listaIdsProduto);
    }
}
