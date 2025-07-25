using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Requests.Login;

namespace CoffeeShop.Application.ExternalServices.Contracts.AWS;
public interface IJwtTokenService
{
    Task<string> AuthenticateUser(LoginValidatedRequest loginValidatedRequest);
}
