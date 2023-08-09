using TinyCRM.Application.Dtos.Permissions;

namespace TinyCRM.Application.Services.Interfaces;

public interface IPermissionService : IService<PermissionContent, int, PermissionDto>
{
    Task<IEnumerable<PermissionDto>> GetForRoleAsync(string role);

    Task<IEnumerable<PermissionDto>> GetForUserAsync(string userId);

    Task GrantToRoleAsync(string role, GrantPermissionDto grantPermissionDto);

    Task GrantToUserAsync(string userId, GrantPermissionDto grantPermissionDto);

    Task UnGrantFromRoleAsync(int id, string role);

    Task UnGrantFromUserAsync(int id, string userId);
}