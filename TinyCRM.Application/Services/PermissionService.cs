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

    public async Task<IEnumerable<PermissionDto>> GetForRoleAsync(string role)
    {
        await CheckRoleExistingAsync(role);

        var permissions = await _permissionRepository.GetGrantedForAsync(new PermissionGrantedForRoleSpecification(role));
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<IEnumerable<PermissionDto>> GetForUserAsync(string userId)
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

    public async Task GrantToRoleAsync(string role, GrantPermissionDto grantPermissionDto)
    {
        await CheckRoleExistingAsync(role);

        await CheckPermissionExistingAsync(grantPermissionDto.PermissionId);

        // Check if permission has already been granted to role
        if (await _permissionGrantRepository.AnyAsync(new PermissionGrantedForRoleSpecification(role, grantPermissionDto.PermissionId)))
        {
            throw new PermissionAlreadyGrantedException(grantPermissionDto.PermissionId);
        }

        await _permissionGrantRepository.InsertForRoleAsync(role, grantPermissionDto.PermissionId);
        await _unitOfWork.CommitAsync();

        // Remove permissions saved in cache
        await _permissionCacheManager.ClearPermissionForRoleAsync(role);
    }

    public async Task UnGrantFromRoleAsync(int id, string role)
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

    public async Task GrantToUserAsync(string userId, GrantPermissionDto grantPermissionDto)
    {
        await CheckUserExistingAsync(userId);

        await CheckPermissionExistingAsync(grantPermissionDto.PermissionId);

        // Check if permission has already been granted to user
        if (await _permissionGrantRepository.AnyAsync(new PermissionGrantedForUserSpecification(userId, grantPermissionDto.PermissionId)))
        {
            throw new PermissionAlreadyGrantedException(grantPermissionDto.PermissionId);
        }

        await _permissionGrantRepository.InsertForUserAsync(userId, grantPermissionDto.PermissionId);
        await _unitOfWork.CommitAsync();

        // Remove permissions saved in cache
        await _permissionCacheManager.ClearPermissionForUserAsync(userId);
    }

    public async Task UnGrantFromUserAsync(int id, string userId)
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