namespace CoffeeShop.Application.UseCase.Email.EmailServiceNotification
{
    public interface IEmailSenderNotificationUseCase
    {
        Task SendEmailNotification(string userEmail);
    }
}
