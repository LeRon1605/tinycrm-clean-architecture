using Microsoft.Extensions.Options;

namespace TinyCRM.Application.Cache;

public class PermissionCacheManager : IPermissionCacheManager
{
    private readonly ICacheService _cacheService;
    private readonly JwtSetting _jwtSetting;

    public PermissionCacheManager(ICacheService cacheService, IOptions<JwtSetting> jwtSetting)
    {
        _cacheService = cacheService;
        _jwtSetting = jwtSetting.Value;
    }

    public Task<IEnumerable<string>?> GetForRoleAsync(string role)
    {
        return _cacheService.GetRecordAsync<IEnumerable<string>?>(KeyGenerator.Generate(CacheTarget.PermissionRole, role));
    }

    public Task<IEnumerable<string>?> GetForUserAsync(string userId)
    {
        return _cacheService.GetRecordAsync<IEnumerable<string>?>(KeyGenerator.Generate(CacheTarget.PermissionUser, userId));
    }

    public Task SetForUserAsync(string userId, IEnumerable<string> permissions)
    {
        return _cacheService.SetRecordAsync(KeyGenerator.Generate(CacheTarget.PermissionUser, userId), permissions, TimeSpan.FromMinutes(_jwtSetting.Expires));
    }

    public Task SetForRoleAsync(string role, IEnumerable<string> permissions)
    {
        return _cacheService.SetRecordAsync(KeyGenerator.Generate(CacheTarget.PermissionRole, role), permissions, TimeSpan.FromMinutes(_jwtSetting.Expires));
    }

    public Task ClearForUserAsync(string userId)
    {
        return _cacheService.RemoveRecordAsync(KeyGenerator.Generate(CacheTarget.PermissionUser, userId));
    }

    public Task ClearForRoleAsync(string role)
    {
        return _cacheService.RemoveRecordAsync(KeyGenerator.Generate(CacheTarget.PermissionRole, role));
    }
}