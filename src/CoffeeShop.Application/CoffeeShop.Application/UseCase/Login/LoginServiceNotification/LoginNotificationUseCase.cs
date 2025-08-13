using CoffeeShop.Application.Services.ExternalServices.Contracts.AWS;
using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Domain.Repositories.Especificas;
using CoffeeShop.Domain.Repositories.LocalRepository;

namespace CoffeeShop.Application.UseCase.Login.Login
{
    public class LoginNotificationUseCase : ILoginNotificationUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICacheService _cacheService;
        private readonly ISesEmailService _sesEmailService;
        public LoginNotificationUseCase(
            IUserRepository userRepository,
            ICacheService cacheService,
            ISesEmailService sesEmailService)
        {
            _userRepository = userRepository;
            _cacheService = cacheService;
            _sesEmailService = sesEmailService;
        }

        public async Task LoginNotification(LoginRequest request)
        {
            var user = await _userRepository.ObterPorPropriedadeAsync(
                u => u.SenServiceEmailNotifications.SenNmEmail == request.UsrEmailNm,
                u => u.SenServiceEmailNotifications
            );

            if (user == null || user.UsrNmPassword != request.UsrNmPassword)
                throw new Exception("Senha inválida.");

            var code = new Random().Next(100000, 999999).ToString();
            await _cacheService.StoreCodeAsync(user.SenServiceEmailNotifications.SenNmEmail, code, TimeSpan.FromMinutes(5));

            var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));

            var templatePath = Path.Combine(
                projectRoot,
                "CoffeeShop.Application",
                "CoffeeShop.Application",
                "EmailTemplate",
                "LoginCodeTemplate.html"
            );

            // Lendo o HTML do template
            var htmlContent = await File.ReadAllTextAsync(templatePath);

            // Substituindo o placeholder pelo código real
            htmlContent = htmlContent.Replace("{{codigo}}", code);

            await _sesEmailService.SendEmailAsync(
                user.SenServiceEmailNotifications.SenNmEmail,
                "Coffee-Shop: Código de Verificação",
                htmlContent
            );
        }
    }
}
