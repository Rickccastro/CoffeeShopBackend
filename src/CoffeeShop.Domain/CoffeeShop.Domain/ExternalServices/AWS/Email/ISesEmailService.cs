namespace CoffeeShop.Domain.ExternalServices.AWS.Email
{
    public interface ISesEmailService
    {
        Task SendEmailAsync(string toAddress, string subject, string body);
    }
}
