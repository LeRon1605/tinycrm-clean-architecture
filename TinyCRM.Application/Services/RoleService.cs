using TinyCRM.Application.Dtos.Roles;
using TinyCRM.Application.UnitOfWorks;
using TinyCRM.Domain.Common.Constants;
using TinyCRM.Domain.Exceptions.Roles;

namespace TinyCRM.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleManager _roleManager;
    private readonly IPermissionGrantRepository _permissionGrantRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IRoleManager roleManager, IPermissionGrantRepository permissionGrantRepository, IUnitOfWork unitOfWork)
    {
        _roleManager = roleManager;
        _permissionGrantRepository = permissionGrantRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResultDto<RoleDto>> GetPagedAsync(RoleFilterAndPagingRequestDto filterParam)
    {
        var (data, total) = await _roleManager.GetPagedAsync(filterParam);
        return new PagedResultDto<RoleDto>()
        {
            Data = data,
            TotalPages = (int)Math.Ceiling(total * 1.0 / filterParam.Size)
        };
    }

    public Task<RoleDto> GetAsync(string id)
    {
        return _roleManager.FindByIdAsync(id);
    }

    public Task<RoleDto> CreateAsync(RoleCreateDto userCreateDto)
    {
        return _roleManager.CreateAsync(userCreateDto);
    }

    public async Task<RoleDto> UpdateAsync(string id, RoleUpdateDto userUpdateDto)
    {
        var role = await GetAsync(id);

        CheckBasicRole(role.Name);

        return await _roleManager.UpdateAsync(id, userUpdateDto);
    }

    public async Task DeleteAsync(string id)
    {
        var role = await GetAsync(id);

        CheckBasicRole(role.Name);

        await _roleManager.DeleteAsync(id);
        await _permissionGrantRepository.RemoveByRoleAsync(role.Name);
        await _unitOfWork.CommitAsync();
    }

    private void CheckBasicRole(string role)
    {
        if (string.Equals(role, AppRole.Admin, StringComparison.CurrentCultureIgnoreCase) ||
            string.Equals(role, AppRole.User, StringComparison.CurrentCultureIgnoreCase))
        {
            throw new BasicRoleAccessDeniedException();
        }
    }
}