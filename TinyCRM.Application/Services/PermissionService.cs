using TinyCRM.Application.Dtos.Permissions;
using TinyCRM.Application.UnitOfWorks;
using TinyCRM.Domain.Exceptions.Permissions;
using TinyCRM.Domain.Specifications.Permissions;

namespace TinyCRM.Application.Services;

public class PermissionService : BaseService<PermissionContent, int, PermissionDto>, IPermissionService
{
    private readonly IApplicationUserManager _userManager;
    private readonly IRoleManager _roleManager;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPermissionGrantRepository _permissionGrantRepository;
    private readonly IPermissionCacheManager _permissionCacheManager;

    public PermissionService(
        IPermissionCacheManager permissionCacheManager,
        IApplicationUserManager userManager,
        IPermissionGrantRepository permissionGrantRepository,
        IRoleManager roleManager,
        IMapper mapper,
        IPermissionRepository repository,
        IUnitOfWork unitOfWork) : base(mapper, repository, unitOfWork)
    {
        _permissionCacheManager = permissionCacheManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _permissionRepository = repository;
        _permissionGrantRepository = permissionGrantRepository;
    }

    public async Task<IEnumerable<PermissionDto>> GetPermissionsForRoleAsync(string roleName)
    {
        await CheckRoleExistingAsync(roleName);

        var permissions = await _permissionRepository.GetGrantedForAsync(new PermissionGrantedForRoleSpecification(roleName));
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<IEnumerable<PermissionDto>> GetPermissionsForUserAsync(string userId)
    {
        await CheckUserExistingAsync(userId);

        // Get all permissions granted for user
        var permissions = await _permissionRepository.GetGrantedForAsync(new PermissionGrantedForUserSpecification(userId));

        // Get all permissions granted for roles of user
        foreach (var role in await _roleManager.GetRolesForUserAsync(userId))
        {
            var specification = new PermissionGrantedForRoleSpecification(role).And(new PermissionGrantedExceptSpecification(permissions));
            var rolePermissions = await _permissionRepository.GetGrantedForAsync(specification);

            permissions = permissions.Concat(rolePermissions).ToList();
        }

        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task AddPermissionToRoleAsync(string roleName, AddPermissionDto addPermissionDto)
    {
        await CheckRoleExistingAsync(roleName);

        await CheckPermissionExistingAsync(addPermissionDto.PermissionId);

        // Check if permission has already been granted to role
        if (await _permissionGrantRepository.AnyAsync(new PermissionGrantedForRoleSpecification(roleName, addPermissionDto.PermissionId)))
        {
            throw new PermissionAlreadyGrantedException(addPermissionDto.PermissionId);
        }

        await _permissionGrantRepository.InsertForRoleAsync(roleName, addPermissionDto.PermissionId);
        await _unitOfWork.CommitAsync();

        // Remove permissions saved in cache
        await _permissionCacheManager.ClearPermissionForRoleAsync(roleName);
    }

    public async Task RemovePermissionFromRoleAsync(int id, string role)
    {
        await CheckRoleExistingAsync(role);

        await CheckPermissionExistingAsync(id);

        var permissionGrant = await _permissionGrantRepository.FindAsync(new PermissionGrantedForRoleSpecification(role, id));
        if (permissionGrant == null)
        {
            throw new PermissionNotGrantedException(id);
        }

        _permissionGrantRepository.Delete(permissionGrant);
        await _unitOfWork.CommitAsync();

        // Remove permissions saved in cache
        await _permissionCacheManager.ClearPermissionForRoleAsync(role);
    }

    public async Task AddPermissionToUserAsync(string userId, AddPermissionDto addPermissionDto)
    {
        await CheckUserExistingAsync(userId);

        await CheckPermissionExistingAsync(addPermissionDto.PermissionId);

        // Check if permission has already been granted to user
        if (await _permissionGrantRepository.AnyAsync(new PermissionGrantedForUserSpecification(userId, addPermissionDto.PermissionId)))
        {
            throw new PermissionAlreadyGrantedException(addPermissionDto.PermissionId);
        }

        await _permissionGrantRepository.InsertForUserAsync(userId, addPermissionDto.PermissionId);
        await _unitOfWork.CommitAsync();

        // Remove permissions saved in cache
        await _permissionCacheManager.ClearPermissionForUserAsync(userId);
    }

    public async Task RemovePermissionFromUserAsync(int id, string userId)
    {
        await CheckUserExistingAsync(userId);

        await CheckPermissionExistingAsync(id);

        var permissionGrant = await _permissionGrantRepository.FindAsync(new PermissionGrantedForUserSpecification(userId, id));
        if (permissionGrant == null)
        {
            throw new PermissionNotGrantedException(id);
        }

        _permissionGrantRepository.Delete(permissionGrant);
        await _unitOfWork.CommitAsync();

        // Remove permissions saved in cache
        await _permissionCacheManager.ClearPermissionForUserAsync(userId);
    }

    private async Task CheckRoleExistingAsync(string roleName)
    {
        if (!await _roleManager.IsExistAsync(roleName))
        {
            throw new ResourceNotFoundException("Role", "name", roleName);
        }
    }

    private async Task CheckUserExistingAsync(string userId)
    {
        if (!await _userManager.IsExistAsync(userId))
        {
            throw new ResourceNotFoundException("User", userId);
        }
    }

    private async Task CheckPermissionExistingAsync(int permissionId)
    {
        if (!await _permissionRepository.IsExistingAsync(permissionId))
        {
            throw new ResourceNotFoundException("Permission", permissionId);
        }
    }
}