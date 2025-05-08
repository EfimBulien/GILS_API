namespace GilsApi.Services;

public interface IRedisCacheService
{
    Task SetCacheAsync(string key, object value, TimeSpan expiration);
    Task<string?> GetCacheAsync(string key);
    Task RemoveCacheAsync(string key);
}
