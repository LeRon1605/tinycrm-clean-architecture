using TinyCRM.Application.Dtos.Roles;

namespace TinyCRM.Application.Services.Interfaces;

public interface IRoleService
{
    Task<PagedResultDto<RoleDto>> GetPagedAsync(RoleFilterAndPagingRequestDto filterParam);

    Task<RoleDto> GetAsync(string id);

    Task<RoleDto> CreateAsync(RoleCreateDto userCreateDto);

    Task<RoleDto> UpdateAsync(string id, RoleUpdateDto userUpdateDto);

    Task DeleteAsync(string id);
}