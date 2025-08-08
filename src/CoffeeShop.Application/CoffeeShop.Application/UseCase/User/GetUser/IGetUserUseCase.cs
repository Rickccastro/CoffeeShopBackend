using CoffeeShop.Domain.Entities;

namespace CoffeeShop.Application.UseCase.User.GetUser;
public interface IGetUserUseCase
{
    public Task<UsrUser> GetUser(Guid userId);
}
