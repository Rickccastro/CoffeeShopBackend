using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class ProdutoRepository : RepositoryBase<ProProduto>, IProdutoRepository
    {
        public ProdutoRepository(CoffeeShopDbContext context): base(context)
        {
        }

        public async Task<ProProduto> ObterPorIdStringAsync(string id)
        {
            try
            {
                var produto = await _context.ProProdutos
                    .AsNoTracking()
                    .Include(p => p.PriPrices) // Apenas a navegação de preços
                    .FirstOrDefaultAsync(p => p.ProIdProduto == id, CancellationToken.None);

                if (produto == null)
                {
                    Console.WriteLine($"[DEBUG] Produto com ID '{id}' não encontrado no banco.");
                    return null;
                }

                return produto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO] Falha ao buscar produto com ID '{id}': {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }
}
