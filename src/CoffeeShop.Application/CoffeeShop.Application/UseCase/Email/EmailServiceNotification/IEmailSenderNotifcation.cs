namespace CoffeeShop.Application.UseCase.Email.EmailServiceNotification
{
    public interface IEmailSenderNotification
    {
        Task SendEmailNotification(string userEmail);
    }
}
