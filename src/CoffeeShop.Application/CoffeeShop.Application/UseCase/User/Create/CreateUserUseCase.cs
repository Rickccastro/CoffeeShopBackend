using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Requests.User;
using CoffeeShop.Communication.Responses;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;



namespace CoffeeShop.Application.UseCase.Customer.Create
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserUseCase(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserResponse> CreateUser(UserRequest request)
        {
                var userId = Guid.NewGuid();

                var usuario = new UsrUsuario
                   {
                    UsrId = userId,
                    UsrNm = request.Nome,
                    UsrNmEndereco = request.Endereco,
                    UsrIntCpf = request.Cpf,
                    UsrIntPassword = Convert.ToUInt32(request.Senha),

                    UsrEmail = new EsnEmailServicoNotificacao
                    {
                        EmailId = Guid.NewGuid(),
                        EmailNm = request.Email,
                        EmailUsrId = userId
                    }
                };

                await _userRepository.AdicionarAsync(usuario);
                await _unitOfWork.Commit();

                return new UserResponse
                {
                    Nome = request.Nome,
                    Cpf = request.Cpf,
                    Endereco = request.Endereco,
                    Email = request.Email,
                    Senha = request.Senha
                };            
        }
    }
}
