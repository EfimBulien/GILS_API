using System.Text.Json;
using StackExchange.Redis;

namespace GilsApi.Services;

public interface IRedisCacheService
{
    Task SetCacheAsync(string key, object value, TimeSpan expiration);
    Task<string?> GetCacheAsync(string key);
    Task RemoveCacheAsync(string key);
}

public class RedisCacheService(IConnectionMultiplexer redis) : IRedisCacheService
{
    private readonly IDatabase _cache = redis.GetDatabase();

    public async Task SetCacheAsync(string key, object value, TimeSpan expiration)
    {
        var jsonData = JsonSerializer.Serialize(value);
        await _cache.StringSetAsync(key, jsonData, expiration);
    }

    public async Task<string?> GetCacheAsync(string key)
    {
        return await _cache.StringGetAsync(key);
    }

    public async Task RemoveCacheAsync(string key)
    {
        await _cache.KeyDeleteAsync(key);
    }
}