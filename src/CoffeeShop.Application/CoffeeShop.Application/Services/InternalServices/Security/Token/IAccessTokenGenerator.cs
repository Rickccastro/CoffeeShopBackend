using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.Services.InternalServices.Security.Token;
public interface IAccessTokenGenerator
{
    string Generate(UsrUser user);

}
