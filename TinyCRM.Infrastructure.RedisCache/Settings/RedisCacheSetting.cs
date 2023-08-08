namespace TinyCRM.Infrastructure.RedisCache.Settings;

public class RedisCacheSetting
{
    public string Prefix { get; set; }
    public int DefaultExpireMinutes { get; set; }
}