using TinyCRM.Application.Dtos.Roles;

namespace TinyCRM.API.Controllers;

[Route("api/roles")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Roles.View)]
    [ProducesResponseType(typeof(PagedResultDto<RoleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRolesAsync([FromQuery] RoleFilterAndPagingRequestDto roleFilterAndPagingRequestDto)
    {
        var roles = await _roleService.GetPagedAsync(roleFilterAndPagingRequestDto);
        return Ok(roles);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Roles.Create)]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateRoleAsync(RoleCreateDto roleCreateDto)
    {
        var role = await _roleService.CreateAsync(roleCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = role.Id }, role);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.Roles.View)]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(string id)
    {
        var role = await _roleService.GetAsync(id);
        return Ok(role);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Roles.Edit)]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateRoleAsync(string id, RoleUpdateDto roleUpdateDto)
    {
        var role = await _roleService.UpdateAsync(id, roleUpdateDto);
        return Ok(role);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Roles.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteRoleAsync(string id)
    {
        await _roleService.DeleteAsync(id);
        return NoContent();
    }
}