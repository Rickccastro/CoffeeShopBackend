using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Requests.User;
using CoffeeShop.Communication.Responses;


namespace CoffeeShop.Application.UseCase.Customer.Create
{
    public interface ICreateUserUseCase
    {
        Task<UserResponse> CreateUser(UserRequest request);
    }
}
