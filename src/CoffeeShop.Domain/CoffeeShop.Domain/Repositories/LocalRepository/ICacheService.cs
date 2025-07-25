namespace CoffeeShop.Domain.Repositories.LocalRepository;
public interface ICacheService
{
    Task StoreCodeAsync(string key, string value, TimeSpan expiration);
    Task<string> GetCodeAsync(string key);
    Task RemoveAsync(string key);
}