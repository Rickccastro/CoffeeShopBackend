using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class PrecoRepository : RepositoryBase<PriPrice>,IPrecoRepository
    {
        public PrecoRepository(CoffeeShopDbContext context) : base(context)
        {
        }

        public async Task<PriPrice> ObterPrecoVigenteAsync(string produtoId, DateTime dataReferencia)
        {
            return await _context.PriPrices
              .AsNoTracking()
              .Where(p => p.PriIdProduto == produtoId &&
                          p.PriDataInicio <= dataReferencia &&
                          (p.PriDataFim == null || p.PriDataFim >= dataReferencia))
              .OrderByDescending(p => p.PriDataInicio)
              .FirstOrDefaultAsync();
        }
    }
}
