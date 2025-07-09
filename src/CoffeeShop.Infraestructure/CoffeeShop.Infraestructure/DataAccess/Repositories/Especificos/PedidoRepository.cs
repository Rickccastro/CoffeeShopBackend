using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class PedidoRepository : RepositoryBase<PedPedido>, IPedidoRepository
    {
        public PedidoRepository(CoffeeShopDbContext context) : base(context)
        {
        }
    }
}
