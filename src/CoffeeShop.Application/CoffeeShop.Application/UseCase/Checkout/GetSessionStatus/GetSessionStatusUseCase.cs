using CoffeeShop.Application.ExternalServices.Contracts.Stripe;
using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using CoffeeShop.Communication.Requests.Checkout;


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
