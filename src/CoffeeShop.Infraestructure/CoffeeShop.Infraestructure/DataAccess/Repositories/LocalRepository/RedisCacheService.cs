using CoffeeShop.Domain.Repositories.LocalRepository;
using StackExchange.Redis;


namespace CoffeeShop.Infraestructure.DataAccess.Repositories.LocalRepository;
public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task StoreCodeAsync(string key, string value, TimeSpan expiration)
    {
        await _database.StringSetAsync(key, value, expiration);
    }

    public async Task<string> GetCodeAsync(string key)
    {
        return await _database.StringGetAsync(key);
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }
}
