namespace TinyCRM.Application.Cache.Interfaces;

public interface IPermissionCacheManager
{
    Task<IEnumerable<string>?> GetPermissionForRoleAsync(string role);

    Task<IEnumerable<string>?> GetPermissionForUserAsync(string userId);

    Task SetPermissionForUserAsync(string userId, IEnumerable<string> permissions, TimeSpan expireTime);

    Task SetPermissionForRoleAsync(string role, IEnumerable<string> permissions, TimeSpan expireTime);

    Task ClearPermissionForUserAsync(string userId);

    Task ClearPermissionForRoleAsync(string role);
}