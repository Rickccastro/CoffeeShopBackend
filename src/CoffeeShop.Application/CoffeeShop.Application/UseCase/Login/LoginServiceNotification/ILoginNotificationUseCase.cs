using CoffeeShop.Communication.Requests.Customer;

namespace CoffeeShop.Application.UseCase.Login.Login
{
    public interface ILoginNotificationUseCase
    {
        Task LoginNotification(LoginRequest request);
    }
}
