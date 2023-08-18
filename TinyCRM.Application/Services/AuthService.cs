using Microsoft.Extensions.Options;
using TinyCRM.Application.Dtos.Auth;
using TinyCRM.Domain.Specifications.Permissions;

namespace TinyCRM.Application.Services;

public class AuthService : IAuthService
{
    private readonly IRoleManager _roleManager;
    private readonly IPermissionCacheManager _permissionCacheManager;
    private readonly ISignInManager _signInManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPermissionRepository _permissionRepository;

    public AuthService(
        IPermissionCacheManager permissionCacheManager,
        IRoleManager roleManager,
        ISignInManager signInManager,
        ITokenProvider tokenProvider,
        IPermissionRepository permissionRepository)
    {
        _permissionCacheManager = permissionCacheManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _tokenProvider = tokenProvider;
        _permissionRepository = permissionRepository;
    }

    public async Task<AuthCredentialDto> SignInAsync(LoginDto loginDto)
    {
        var claims = await _signInManager.SignInAsync(loginDto.UserNameOrEmail, loginDto.Password);

        return new AuthCredentialDto()
        {
            AccessToken = _tokenProvider.GenerateAccessToken(claims)
        };
    }

    public async Task<IEnumerable<string>> GetPermissionsForUserAsync(string userId)
    {
        var permissions = new List<string>();

        // Get permissions from user roles
        foreach (var role in await _roleManager.GetRolesForUserAsync(userId))
        {
            var rolePermissions = await _permissionCacheManager.GetForRoleAsync(role);
            if (rolePermissions == null)
            {
                rolePermissions = (await _permissionRepository.GetGrantedForAsync(new PermissionGrantedForRoleSpecification(role))).Select(x => x.Name);
                await _permissionCacheManager.SetForRoleAsync(role, rolePermissions);
            }

            permissions.AddRange(rolePermissions);
        }

        // Get permissions from user
        var userPermissions = await _permissionCacheManager.GetForUserAsync(userId);
        if (userPermissions == null)
        {
            userPermissions = (await _permissionRepository.GetGrantedForAsync(new PermissionGrantedForUserSpecification(userId))).Select(x => x.Name);
            await _permissionCacheManager.SetForUserAsync(userId, userPermissions);
        }
        permissions.AddRange(userPermissions);

        return permissions;
    }
}