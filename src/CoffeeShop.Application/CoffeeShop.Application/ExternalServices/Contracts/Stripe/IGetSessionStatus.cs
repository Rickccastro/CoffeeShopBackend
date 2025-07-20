
using CoffeeShop.Application.ExternalServices.DTO.Stripe;

namespace CoffeeShop.Application.ExternalServices.Contracts.Stripe;
    public interface IGetSessionStatus
    {
        SessionStatus GetStatus(string sessionId);
    }

