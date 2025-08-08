namespace CoffeeShop.Communication.Requests.Customer
{
    public class LoginRequest
    {
        public required string UsrEmailNm { get; set; }
        public required string UsrNmPassword { get; set; }
    }
}
