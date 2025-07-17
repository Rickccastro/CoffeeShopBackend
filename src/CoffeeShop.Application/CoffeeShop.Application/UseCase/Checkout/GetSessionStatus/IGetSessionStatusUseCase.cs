using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;

namespace CoffeeShop.Application.UseCase.Checkout.GetSessionStatus
{
    public interface IGetSessionStatusUseCase
    {
        SessionStatus GetStatus(SessionRequest sessionRequest);
    }
}
