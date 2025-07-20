using CoffeeShop.Application.ExternalServices.Contracts.Stripe;
using CoffeeShop.Application.ExternalServices.DTO.Stripe;
using Stripe.Checkout;

namespace CoffeeShop.Infraestructure.ExternalServices.Stripe.GetSession
{
    public class GetSessionStatus : IGetSessionStatus
    {
        public SessionStatus GetStatus(string sessionId)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(sessionId);

            return new SessionStatus
            {
                Status = session.Status,
                CustomerEmail = session.CustomerDetails?.Email
            };
        }
    }
}
