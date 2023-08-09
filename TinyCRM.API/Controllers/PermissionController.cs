using TinyCRM.Application.Dtos.Permissions;

namespace TinyCRM.API.Controllers;

[ApiController]
[Route("api/permissions")]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.PermissionManagement.View)]
    [ProducesResponseType(typeof(PagedResultDto<PermissionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPermissionsAsync([FromQuery] PermissionFilterAndPagingRequestDto permissionFilterAndPagingRequestDto)
    {
        var permissions = await _permissionService.GetPagedAsync(permissionFilterAndPagingRequestDto);
        return Ok(permissions);
    }

    [HttpGet("roles/{roleName}")]
    [Authorize(Policy = Permissions.PermissionManagement.View)]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPermissionsForRoleAsync(string roleName)
    {
        return Ok(await _permissionService.GetForRoleAsync(roleName));
    }

    [HttpPost("roles/{roleName}")]
    [Authorize(Policy = Permissions.PermissionManagement.GrantToRole)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> GrantPermissionToRoleAsync(string roleName, GrantPermissionDto grantPermissionDto)
    {
        await _permissionService.GrantToRoleAsync(roleName, grantPermissionDto);
        return NoContent();
    }

    [HttpDelete("{id}/roles/{roleName}")]
    [Authorize(Policy = Permissions.PermissionManagement.UnGrantFromUser)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnGrantPermissionsForRoleAsync(int id, string roleName)
    {
        await _permissionService.UnGrantFromRoleAsync(id, roleName);
        return NoContent();
    }

    [HttpGet("users/{userId}")]
    [Authorize(Policy = Permissions.PermissionManagement.View)]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPermissionsForUserAsync(string userId)
    {
        var permissions = await _permissionService.GetForUserAsync(userId);
        return Ok(permissions);
    }

    [HttpDelete("{id}/users/{userId}")]
    [Authorize(Policy = Permissions.PermissionManagement.UnGrantFromUser)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnGrantPermissionsForUserAsync(int id, string userId)
    {
        await _permissionService.UnGrantFromUserAsync(id, userId);
        return NoContent();
    }

    [HttpPost("users/{userId}")]
    [Authorize(Policy = Permissions.PermissionManagement.GrantToRole)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> GrantToUserAsync(string userId, GrantPermissionDto grantPermissionDto)
    {
        await _permissionService.GrantToUserAsync(userId, grantPermissionDto);
        return NoContent();
    }
}