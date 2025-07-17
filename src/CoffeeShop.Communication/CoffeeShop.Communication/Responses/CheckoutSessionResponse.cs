namespace CoffeeShop.Communication.Responses
{
    public class CheckoutSessionResponse
    {
        public string ClientSecret { get; set; }

        public CheckoutSessionResponse(string clientSecret)
        {
            ClientSecret = clientSecret;
        }
    }
}
