using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class ProdutoRepository : RepositoryBase<ProProduto>, IProdutoRepository
    {
        public ProdutoRepository(CoffeeShopDbContext context): base(context)
        {
        }
    }
}
