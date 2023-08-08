using StackExchange.Redis;
using TinyCRM.Application.Cache.Interfaces;
using TinyCRM.Application.Cache;
using TinyCRM.Infrastructure.RedisCache;

namespace TinyCRM.API.Extensions;

public static partial class DependencyInjectionExtensions
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        var multiplexer = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
        services.AddTransient<ICacheService, RedisCacheService>();
        services.AddTransient<IPermissionCacheManager, PermissionCacheManager>();

        return services;
    }
}