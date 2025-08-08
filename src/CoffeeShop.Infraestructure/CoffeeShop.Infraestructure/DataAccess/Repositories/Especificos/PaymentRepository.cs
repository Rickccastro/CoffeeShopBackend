using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos;
public class PaymentRepository : RepositoryBase<PayPayment>, IPaymentsRepository
{
    public PaymentRepository(CoffeeShopDbContext context) : base(context)
    {
    }
}


