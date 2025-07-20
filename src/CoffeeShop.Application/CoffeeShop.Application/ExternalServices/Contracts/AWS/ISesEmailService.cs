namespace CoffeeShop.Application.ExternalServices.Contracts.AWS;


public interface ISesEmailService
{
    Task SendEmailAsync(string toAddress, string subject, string body);
}
