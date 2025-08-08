using CoffeeShop.Application.Services.ExternalServices.Contracts.AWS;

namespace CoffeeShop.Application.UseCase.Email.EmailServiceNotification
{
    public class EmailSenderNotificationUseCase : IEmailSenderNotificationUseCase
    {
        private readonly ISesEmailService _sesEmailService;

        public EmailSenderNotificationUseCase(ISesEmailService sesEmailService)
        {
            _sesEmailService = sesEmailService; 
        }
        public async Task SendEmailNotification(string userEmail)
        {
            var subject = "Bem-vindo!";
            var body = "<p>Obrigado por se cadastrar!</p>";
            await _sesEmailService.SendEmailAsync(userEmail, subject, body);
        }
    }
}
