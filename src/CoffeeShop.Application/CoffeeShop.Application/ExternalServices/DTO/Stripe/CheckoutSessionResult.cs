namespace CoffeeShop.Application.ExternalServices.DTO.Stripe;
public class CheckoutSessionResult
{
    public string SessionId { get; set; }
    public string ClientSecret { get; set; }
    public string Status { get; set; }
    public string PaymentIntentId { get; set; }
    public string ReturnUrl { get; set; }
}
