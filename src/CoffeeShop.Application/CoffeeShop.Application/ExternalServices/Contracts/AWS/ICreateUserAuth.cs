namespace CoffeeShop.Application.ExternalServices.Contracts.AWS;
public interface ICreateUserAuth
{
    Task CreateAdminUser(string email, string password);
}
