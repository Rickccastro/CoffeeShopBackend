using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class OrderRepository : RepositoryBase<OrdOrder>, IOrderRepository
    {
        public OrderRepository(CoffeeShopDbContext context) : base(context) { }

        public void AttachProdutoAndPrice(IEnumerable<OriOrderItem> pedidoItens)
        {
            AttachEntities(pedidoItens.Select(pi => pi.OriIdPriceNavigation));
            AttachEntities(pedidoItens.Select(pi => pi.OriIdProductNavigation));
        }
    }
}
