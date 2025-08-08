using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.Services.InternalServices;
public interface ILoggedUser
{
    Task<UsrUser> Get();
}
