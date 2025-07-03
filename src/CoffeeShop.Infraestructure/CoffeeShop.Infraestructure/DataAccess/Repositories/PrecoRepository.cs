using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Infraestructure.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories
{
    public class PrecoRepository : IPrecoRepository
    {
        private readonly CoffeeShopDbContext _context;

        public PrecoRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public async Task<PriPrice> ObterPrecoVigenteAsync(Guid produtoId, DateTime dataReferencia)
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
