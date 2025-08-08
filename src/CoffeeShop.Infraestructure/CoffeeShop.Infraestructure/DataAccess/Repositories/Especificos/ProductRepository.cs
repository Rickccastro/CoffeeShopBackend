using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class ProductRepository : RepositoryBase<ProProduct>, IProductRepository
    {
        public ProductRepository(CoffeeShopDbContext context) : base(context)
        {
        }

        public async Task<List<ProProduct>> ObterListaIdsProdutosAsync(List<string> listaIdsProduto)
        {
            return await _context.ProProducts
                .AsNoTracking()
                .Include(p => p.PriPrices)
                .Where(p => listaIdsProduto.Contains(p.ProIdProduct))
                .ToListAsync(CancellationToken.None);
        }
    }
}
