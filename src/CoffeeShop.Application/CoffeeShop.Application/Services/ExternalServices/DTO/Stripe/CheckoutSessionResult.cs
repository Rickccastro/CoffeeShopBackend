using CoffeeShop.Domain.Enums;

namespace CoffeeShop.Application.Services.ExternalServices.DTO.Stripe;
public class CheckoutSessionResult
{
    public string SessionId { get; set; }
    public string ClientSecret { get; set; }
    public string Status { get; set; }
    public string PaymentIntentId { get; set; }
    public string ReturnUrl { get; set; }
    public string CustomerEmail { get; set; }
    public List<string> PaymentMethodTypes { get; set; }
}
