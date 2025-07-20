using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class ProdutoRepository : RepositoryBase<ProProduto>, IProdutoRepository
    {
        public ProdutoRepository(CoffeeShopDbContext context) : base(context)
        {
        }

        public async Task<List<ProProduto>> ObterListaIdsProdutosAsync(List<string> listaIdsProduto)
        {
            return await _context.ProProdutos
                .AsNoTracking()
                .Include(p => p.PriPrices)
                .Where(p => listaIdsProduto.Contains(p.ProIdProduto))
                .ToListAsync(CancellationToken.None);
        }
    }
}
