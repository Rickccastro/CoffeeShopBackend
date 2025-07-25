using CoffeeShop.Application.ExternalServices.Contracts.AWS;
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
                u => u.UsrEmail.EmailNm == request.Email,
                u => u.UsrEmail
            );

            if (user == null || user.UsrIntPassword != Convert.ToInt32(request.Senha))
                throw new Exception("Email ou senha inválidos.");

            var code = new Random().Next(100000, 999999).ToString();
            await _cacheService.StoreCodeAsync(user.UsrEmail.EmailNm, code, TimeSpan.FromMinutes(5));
            await _sesEmailService.SendEmailAsync(user.UsrEmail.EmailNm, "Coffee-Shop: Codigo de Verificação", $"Seu código é: {code}");
        }
    }
}
