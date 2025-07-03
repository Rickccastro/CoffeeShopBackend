using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories;
using CoffeeShop.Infraestructure.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CoffeeShopDbContext _context;

        public ProdutoRepository(CoffeeShopDbContext context)
        {
            _context = context;
        }

        public async Task<ProProduto> ObterPorIdAsync(Guid produtoId)
        {
            return await _context.ProProdutos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProIdProduto == produtoId);
        }
    }
}
