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
    private readonly JwtSetting _jwtSetting;

    public AuthService(
        IPermissionCacheManager permissionCacheManager,
        IRoleManager roleManager,
        ISignInManager signInManager,
        ITokenProvider tokenProvider,
        IPermissionRepository permissionRepository,
        IOptions<JwtSetting> jwtSettingOption)
    {
        _permissionCacheManager = permissionCacheManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _tokenProvider = tokenProvider;
        _permissionRepository = permissionRepository;
        _jwtSetting = jwtSettingOption.Value;
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

        // Get permissions from roles
        foreach (var role in await _roleManager.GetRolesForUserAsync(userId))
        {
            var rolePermissions = await _permissionCacheManager.GetPermissionForRoleAsync(role);
            if (rolePermissions == null)
            {
                rolePermissions = (await _permissionRepository.GetGrantedForAsync(new PermissionGrantedForRoleSpecification(role))).Select(x => x.Name);
                await _permissionCacheManager.SetPermissionForRoleAsync(role, rolePermissions, TimeSpan.FromMinutes(_jwtSetting.Expires));
            }

            permissions.AddRange(rolePermissions);
        }

        // Get permissions from user
        var userPermissions = await _permissionCacheManager.GetPermissionForUserAsync(userId);
        if (userPermissions == null)
        {
            userPermissions = (await _permissionRepository.GetGrantedForAsync(new PermissionGrantedForUserSpecification(userId))).Select(x => x.Name);
            await _permissionCacheManager.SetPermissionForUserAsync(userId, userPermissions, TimeSpan.FromMinutes(_jwtSetting.Expires));
        }
        permissions.AddRange(userPermissions);

        return permissions;
    }
}