using Newtonsoft.Json;
using StackExchange.Redis;
using TinyCRM.Application.Cache.Interfaces;

namespace TinyCRM.Infrastructure.RedisCache;

public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = redis.GetDatabase();
    }

    public async Task<T> GetOrAddAsync<T>(string id, Func<Task<T>> callback, TimeSpan expireTime)
    {
        var data = await GetRecordAsync<T>(id);
        if (data == null)
        {
            data = await callback.Invoke();
            await SetRecordAsync(id, data, expireTime);
        }

        return data;
    }

    public Task<bool> SetRecordAsync<T>(string id, T data, TimeSpan expireTime)
    {
        var serializedData = JsonConvert.SerializeObject(data);

        return _db.StringSetAsync(id, serializedData, expireTime, When.Always);
    }

    public async Task<T?> GetRecordAsync<T>(string id)
    {
        var stringData = await _db.StringGetAsync(id);
        if (string.IsNullOrWhiteSpace(stringData))
        {
            return default;
        }

        return JsonConvert.DeserializeObject<T>(stringData);
    }

    public Task<bool> RemoveRecordAsync(string id)
    {
        return _db.KeyDeleteAsync(id);
    }

    public async Task ClearAsync()
    {
        var endpoints = _redis.GetEndPoints(true);
        foreach (var endpoint in endpoints)
        {
            var server = _redis.GetServer(endpoint);
            await server.FlushAllDatabasesAsync();
        }
    }
}