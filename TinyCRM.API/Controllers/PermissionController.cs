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
        return Ok(await _permissionService.GetPermissionsForRoleAsync(roleName));
    }

    [HttpPost("roles/{roleName}")]
    [Authorize(Policy = Permissions.PermissionManagement.AddToRole)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddPermissionToRoleAsync(string roleName, AddPermissionDto addPermissionDto)
    {
        await _permissionService.AddPermissionToRoleAsync(roleName, addPermissionDto);
        return NoContent();
    }

    [HttpDelete("{id}/roles/{roleName}")]
    [Authorize(Policy = Permissions.PermissionManagement.RemoveFromUser)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemovePermissionsForRoleAsync(int id, string roleName)
    {
        await _permissionService.RemovePermissionFromRoleAsync(id, roleName);
        return NoContent();
    }

    [HttpGet("users/{userId}")]
    [Authorize(Policy = Permissions.PermissionManagement.View)]
    [ProducesResponseType(typeof(IEnumerable<PermissionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllPermissionsForUserAsync(string userId)
    {
        var permissions = await _permissionService.GetPermissionsForUserAsync(userId);
        return Ok(permissions);
    }

    [HttpDelete("{id}/users/{userId}")]
    [Authorize(Policy = Permissions.PermissionManagement.RemoveFromUser)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemovePermissionsForUserAsync(int id, string userId)
    {
        await _permissionService.RemovePermissionFromUserAsync(id, userId);
        return NoContent();
    }

    [HttpPost("users/{userId}")]
    [Authorize(Policy = Permissions.PermissionManagement.AddToRole)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddPermissionToUserAsync(string userId, AddPermissionDto addPermissionDto)
    {
        await _permissionService.AddPermissionToUserAsync(userId, addPermissionDto);
        return NoContent();
    }
}