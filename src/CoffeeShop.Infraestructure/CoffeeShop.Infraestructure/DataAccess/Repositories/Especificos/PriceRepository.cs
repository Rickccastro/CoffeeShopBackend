using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class PriceRepository : RepositoryBase<PriPrice>,IPriceRepository
    {
        public PriceRepository(CoffeeShopDbContext context) : base(context)
        {
        }

        public async Task<PriPrice> ObterPrecoVigenteAsync(string produtoId, DateTime dataReferencia)
        {
            return await _context.PriPrices
              .AsNoTracking()
              .Where(p => p.PriProductId == produtoId &&
                          p.PriDateStart <= dataReferencia &&
                          (p.PriDateEnd == null || p.PriDateEnd >= dataReferencia))
              .OrderByDescending(p => p.PriDateStart)
              .FirstOrDefaultAsync();
        }
    }
}
