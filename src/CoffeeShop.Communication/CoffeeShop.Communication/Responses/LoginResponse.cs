namespace CoffeeShop.Communication.Responses
{
    public class LoginResponse
    {
        public required Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}
