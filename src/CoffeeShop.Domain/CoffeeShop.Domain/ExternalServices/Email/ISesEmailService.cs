namespace CoffeeShop.Domain.Services.Email
{
    public interface ISesEmailService
    {
        Task SendEmailAsync(string toAddress, string subject, string body);
    }
}
