using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;
using System.Linq.Expressions;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class PedidoItensRepository : RepositoryBase<PeiPedidoIten>, IPedidoItensRepository
    {
        public PedidoItensRepository(CoffeeShopDbContext context) : base(context)
        {
        }
    }
}
