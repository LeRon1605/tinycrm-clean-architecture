namespace TinyCRM.Application.Cache;

public class PermissionCacheManager : IPermissionCacheManager
{
    private readonly ICacheService _cacheService;

    public PermissionCacheManager(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public Task<IEnumerable<string>> GetPermissionForRoleAsync(string role)
    {
        return _cacheService.GetRecordAsync<IEnumerable<string>>(KeyGenerator.Generate(CacheTarget.PermissionRole, role));
    }

    public Task<IEnumerable<string>> GetPermissionForUserAsync(string userId)
    {
        return _cacheService.GetRecordAsync<IEnumerable<string>>(KeyGenerator.Generate(CacheTarget.PermissionUser, userId));
    }

    public Task SetPermissionForUserAsync(string userId, IEnumerable<string> permissions, TimeSpan expireTime)
    {
        return _cacheService.SetRecordAsync(KeyGenerator.Generate(CacheTarget.PermissionUser, userId), permissions, expireTime);
    }

    public Task SetPermissionForRoleAsync(string role, IEnumerable<string> permissions, TimeSpan expireTime)
    {
        return _cacheService.SetRecordAsync(KeyGenerator.Generate(CacheTarget.PermissionRole, role), permissions, expireTime);
    }

    public Task ClearPermissionForUserAsync(string userId)
    {
        return _cacheService.RemoveRecordAsync(KeyGenerator.Generate(CacheTarget.PermissionUser, userId));
    }

    public Task ClearPermissionForRoleAsync(string role)
    {
        return _cacheService.RemoveRecordAsync(KeyGenerator.Generate(CacheTarget.PermissionRole, role));
    }
}