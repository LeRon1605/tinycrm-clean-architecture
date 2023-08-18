namespace TinyCRM.Application.Cache.Interfaces;

public interface IPermissionCacheManager
{
    Task<IEnumerable<string>?> GetForRoleAsync(string role);

    Task<IEnumerable<string>?> GetForUserAsync(string userId);

    Task SetForUserAsync(string userId, IEnumerable<string> permissions);

    Task SetForRoleAsync(string role, IEnumerable<string> permissions);

    Task ClearForUserAsync(string userId);

    Task ClearForRoleAsync(string role);
}