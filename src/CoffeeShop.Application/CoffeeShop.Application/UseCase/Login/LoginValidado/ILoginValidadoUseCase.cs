using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Requests.Login;
using CoffeeShop.Communication.Responses;

namespace CoffeeShop.Application.UseCase.Login.LoginValidado;
public interface ILoginValidadoUseCase
{
    Task<LoginResponse> LoginValidado(LoginValidatedRequest loginValidatedRequest);
}
