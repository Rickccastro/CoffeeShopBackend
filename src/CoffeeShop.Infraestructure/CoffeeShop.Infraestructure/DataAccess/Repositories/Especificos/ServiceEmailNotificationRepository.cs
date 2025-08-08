using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class ServiceEmailNotificationRepository : RepositoryBase<SenServiceEmailNotification>, IServiceEmailNotificationRepository
    {
        public ServiceEmailNotificationRepository(CoffeeShopDbContext context) : base(context)
        {
        }
    }
}
