using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Responses;
using CoffeeShop.Domain.Repositories.Especificas;
using System.Data.SqlTypes;

namespace CoffeeShop.Application.UseCase.Customer.Login
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IUserRepository _userRepository;
        public LoginUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<LoginResponse> LoginUser(LoginRequest request)
        {
            var user = await _userRepository.ObterPorPropriedadeAsync(
                u => u.UsrEmail.EmailNm == request.Email,
                u => u.UsrEmail
            );
            
            if (user == null)
            {
                throw new SqlNullValueException("Nenhum Email é vinculado a uma conta.");
            }
            if (user.UsrEmail.EmailNm != request.Email && user.UsrIntPassword != Convert.ToInt32(request.Senha))
            {
                throw new Exception("Email ou Senha Invalidos.");
            }
           return new LoginResponse
            {
                Id = user.UsrId,
                Nome = user.UsrNm,
                Email = user.UsrEmail.EmailNm
            };
        }
    }
}
