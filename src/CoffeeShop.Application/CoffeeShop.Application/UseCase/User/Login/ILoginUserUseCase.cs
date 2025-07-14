using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Responses;

namespace CoffeeShop.Application.UseCase.Customer.Login
{
    public interface ILoginUserUseCase
    {
        Task<LoginResponse> LoginUser(LoginRequest request);
    }
}
