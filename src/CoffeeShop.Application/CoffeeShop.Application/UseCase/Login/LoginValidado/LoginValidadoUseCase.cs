using CoffeeShop.Application.ExternalServices.Contracts.AWS;
using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Requests.Login;
using CoffeeShop.Communication.Responses;
using CoffeeShop.Domain.Repositories.Especificas;
using CoffeeShop.Domain.Repositories.LocalRepository;

namespace CoffeeShop.Application.UseCase.Login.LoginValidado;
internal class LoginValidadoUseCase : ILoginValidadoUseCase
{
    private readonly ICacheService _cacheService;
    private readonly IJwtTokenService _jwtTokenService; 
    private readonly IUserRepository _userRepository;

    public LoginValidadoUseCase(
        ICacheService cacheService,
        IJwtTokenService jwtTokenService,
        IUserRepository userRepository)
    {
        _cacheService = cacheService;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
    }

    public async Task<LoginResponse> LoginValidado(LoginValidatedRequest loginRequest)
    {

        var codeInCache = await _cacheService.GetCodeAsync(loginRequest.Email);

        if (string.IsNullOrEmpty(codeInCache) || codeInCache != loginRequest.Code)
            throw new Exception("Código inválido ou expirado.");

        var user = await _userRepository.ObterPorPropriedadeAsync(
            u => u.UsrEmail.EmailNm == loginRequest.Email,
            u => u.UsrEmail
        );

        var token = await _jwtTokenService.AuthenticateUser(loginRequest);

        return new LoginResponse
        {
            //Id = user.UsrId,
            Nome = user.UsrNm,
            Email = user.UsrEmail.EmailNm,
            Token = token
        };
    }
}
