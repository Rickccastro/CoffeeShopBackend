using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Infraestructure.DataAccess.Repositories.Especificos
{
    public class UserRepository : RepositoryBase<UsrUsuario>, IUserRepository
    {
        public UserRepository(CoffeeShopDbContext context) : base(context)
        {
        }
    }
}
