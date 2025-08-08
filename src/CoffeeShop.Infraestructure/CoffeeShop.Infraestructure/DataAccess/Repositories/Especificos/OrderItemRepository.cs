using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class OrderItemRepository : RepositoryBase<OriOrderItem>, IOrderItemsRepository
    {
        public OrderItemRepository(CoffeeShopDbContext context) : base(context)
        {
        }
      
    }
}
