using CoffeeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Repositories
{
    public interface IProdutoRepository
    {
        Task<ProProduto> ObterPorIdAsync(Guid produtoId);
    }
}
