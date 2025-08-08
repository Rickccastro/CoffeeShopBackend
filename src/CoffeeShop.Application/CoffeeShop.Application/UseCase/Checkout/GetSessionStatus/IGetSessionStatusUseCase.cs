using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
using CoffeeShop.Communication.Requests.Checkout;

namespace CoffeeShop.Application.UseCase.Checkout.GetSessionStatus
{
    public interface IGetSessionStatusUseCase
    {
        SessionStatus GetStatus(SessionRequest sessionRequest);
    }
}
