using System.Text.Json.Serialization;

namespace CoffeeShop.Communication.Responses
{
    public class LoginResponse
    {
        public required string UsrNm { get; set; } = null!;
        public required string EmailNm { get; set; }
        public string? Token { get; set; }
        public string? UserId { get; set; }
    }
}
