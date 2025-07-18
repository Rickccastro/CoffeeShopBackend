using AutoMapper;
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
        private readonly IMapper _mapper;


        public CreateUserUseCase(IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserResponse> CreateUser(UserRequest request)
        {
            var userId = Guid.NewGuid();

            var usuario = _mapper.Map<UsrUsuario>(request);

            _mapper.Map<UserRequest>(usuario);

            await _userRepository.AdicionarAsync(usuario);
            await _unitOfWork.Commit();

            return _mapper.Map<UserResponse>(usuario);
        }
    }
}
