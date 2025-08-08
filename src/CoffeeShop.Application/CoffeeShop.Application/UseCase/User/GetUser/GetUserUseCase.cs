using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Repositories.Especificas;

namespace CoffeeShop.Application.UseCase.User.GetUser;
public class GetUserUseCase : IGetUserUseCase
{
    private readonly IUserRepository _userRepository;

    public GetUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UsrUser> GetUser(Guid userId)
    {
        return await _userRepository.ObterPorIdAsync(userId);
    }
}
