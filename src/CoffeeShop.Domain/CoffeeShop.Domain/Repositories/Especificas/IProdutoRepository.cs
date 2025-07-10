using CoffeeShop.Domain.Entities;
using System.Linq.Expressions;


namespace CoffeeShop.Domain.Repositories.Especificas
{
    public interface IProdutoRepository : IRepositoryBase<ProProduto>
    {
        Task<ProProduto> ObterPorIdStringAsync(string id);
    }
}
