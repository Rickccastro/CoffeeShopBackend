using CoffeeShop.Domain.Services.Email;

namespace CoffeeShop.Application.UseCase.Email.EmailServiceNotification
{
    public class EmailSenderNotification : IEmailSenderNotification
    {
        private readonly ISesEmailService _sesEmailService;

        public EmailSenderNotification(ISesEmailService sesEmailService)
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
