namespace CoffeeShop.Communication.Responses
{
    public class CheckoutSessionResponse
    {
        public string ClientSecret { get; set; }
        public string SessionId { get; set; }

        public CheckoutSessionResponse(string clientSecret, string sessionId)
        {
            ClientSecret = clientSecret;
            SessionId = sessionId;
        }

    }
}
