using TinyCRM.Application.Dtos.Users;

namespace TinyCRM.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Users.View)]
    [ProducesResponseType(typeof(PagedResultDto<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] UserFilterAndPagingRequestDto userFilterAndPagingRequestDto)
    {
        var users = await _userService.GetPagedAsync(userFilterAndPagingRequestDto);
        return Ok(users);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Users.Create)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUserAsync(UserCreateDto userCreateDto)
    {
        var user = await _userService.CreateAsync(userCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.Users.View)]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(string id)
    {
        var user = await _userService.GetAsync(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.Users.Edit)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateUserAsync(string id, UserUpdateDto userUpdateDto)
    {
        var user = await _userService.UpdateAsync(id, userUpdateDto);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.Users.Delete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserAsync(string id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}