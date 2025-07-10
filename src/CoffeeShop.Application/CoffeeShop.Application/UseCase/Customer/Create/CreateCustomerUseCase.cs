using CoffeeShop.Communication.Requests.Customer;
using CoffeeShop.Communication.Responses;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;



namespace CoffeeShop.Application.UseCase.Customer.Create
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerUseCase(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerResponse> CreateCustomer(CustomerRequest request)
        {
            try
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

                return new CustomerResponse
                {
                    Nome = request.Nome,
                    Cpf = request.Cpf,
                    Endereco = request.Endereco,
                    Email = request.Email,
                    Senha = request.Senha
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no Commit: {ex.Message}");
                throw; 
            }
        }
    }
}
