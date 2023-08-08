using System.Security.Claims;

namespace TinyCRM.API.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    private readonly IAuthService _authService;

    public PermissionAuthorizationHandler(IAuthService authService)
    {
        _authService = authService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var permissions = await _authService.GetPermissionsForUserAsync(userId);

        var isAuthorized = permissions.Contains(requirement.Permission);
        if (isAuthorized)
        {
            context.Succeed(requirement);
        }
    }
}