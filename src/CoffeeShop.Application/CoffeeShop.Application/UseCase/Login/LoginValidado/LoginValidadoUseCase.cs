using CoffeeShop.Application.Services.InternalServices.Security.Token;
using CoffeeShop.Communication.Requests.Login;
using CoffeeShop.Communication.Responses;
using CoffeeShop.Domain.Repositories.Especificas;
using CoffeeShop.Domain.Repositories.LocalRepository;

namespace CoffeeShop.Application.UseCase.Login.LoginValidado;
internal class LoginValidadoUseCase : ILoginValidadoUseCase
{
    private readonly ICacheService _cacheService;
    private readonly IUserRepository _userRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;   

    public LoginValidadoUseCase(
        ICacheService cacheService,
        IUserRepository userRepository,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _cacheService = cacheService;
        _userRepository = userRepository;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<LoginResponse> LoginValidado(LoginValidatedRequest loginRequest)
    {

        var codeInCache = await _cacheService.GetCodeAsync(loginRequest.EmailNm);

        if (string.IsNullOrEmpty(codeInCache) || codeInCache != loginRequest.EmailCode)
            throw new Exception("Código inválido ou expirado.");

        var user = await _userRepository.ObterPorPropriedadeAsync(
            u => u.SenServiceEmailNotifications.SenNmEmail == loginRequest.EmailNm,
            u => u.SenServiceEmailNotifications
        );


        return new LoginResponse
        {
            UsrNm = user.UsrNmName,
            Token = _accessTokenGenerator.Generate(user),
            EmailNm = user.SenServiceEmailNotifications!.SenNmEmail,
            UserId = user.UsrIdUser.ToString()
        };
    }
}
