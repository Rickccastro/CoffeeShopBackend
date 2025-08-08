using CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;

namespace CoffeeShop.Application.Services.ExternalServices.Contracts.Stripe;
    public interface IGetSessionStatus
    {
        SessionStatus GetStatus(string sessionId);
    }

