using CoffeeShop.Communication.Requests.Checkout;
using CoffeeShop.Domain.ExternalServices.Stripe;
using CoffeeShop.Domain.ExternalServices.Stripe.Entities;

namespace CoffeeShop.Application.UseCase.Checkout.GetSessionStatus
{
    public class GetSessionStatusUseCase : IGetSessionStatusUseCase
    {
        private readonly IGetSessionStatus _getSessionStatus;

        public GetSessionStatusUseCase(
             IGetSessionStatus getSession)
        {
            _getSessionStatus = getSession;
        }
        public SessionStatus GetStatus(SessionRequest session)
        {
            return _getSessionStatus.GetStatus(session.SessionId);
        }
    }
}
