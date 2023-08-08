using TinyCRM.Application.Dtos.Roles;

namespace TinyCRM.Application.Identity;

public interface IRoleManager
{
    Task<(IEnumerable<RoleDto>, int)> GetPagedAsync(RoleFilterAndPagingRequestDto roleFilterAndPagingRequestDto);

    Task<RoleDto> FindByIdAsync(string id);

    Task<RoleDto> CreateAsync(RoleCreateDto roleCreateDto);

    Task<RoleDto> UpdateAsync(string id, RoleUpdateDto roleUpdateDto);

    Task DeleteAsync(string id);

    Task<bool> IsExistAsync(string role);

    Task<IEnumerable<string>> GetRolesForUserAsync(string userId);
}