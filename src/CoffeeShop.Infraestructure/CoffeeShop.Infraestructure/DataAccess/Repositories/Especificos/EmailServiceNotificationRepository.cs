using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class EmailServiceNotificationRepository : RepositoryBase<EsnEmailServicoNotificacao>, IEmailServiceNotificationRepository
    {
        public EmailServiceNotificationRepository(CoffeeShopDbContext context) : base(context)
        {
        }
    }
}
