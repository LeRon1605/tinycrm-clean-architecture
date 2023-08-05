using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Dtos.Users;
using TinyCRM.Application.Services.Abstracts;
using TinyCRM.Domain.Common.Constants;

namespace TinyCRM.API.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(PagedResultDto<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] UserFilterAndPagingRequestDto userFilterAndPagingRequestDto)
    {
        var userDtos = await _userService.GetPagedAsync(userFilterAndPagingRequestDto);
        return Ok(userDtos);
    }

    [HttpPost]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUserAsync(UserCreateDto userCreateDto)
    {
        var userDto = await _userService.CreateAsync(userCreateDto);
        return CreatedAtAction(nameof(GetDetailAsync), new { id = userDto.Id }, userDto);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ActionName(nameof(GetDetailAsync))]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailAsync(string id)
    {
        var userDto = await _userService.GetAsync(id);
        return Ok(userDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateUserAsync(string id, UserUpdateDto userUpdateDto)
    {
        var userDto = await _userService.UpdateAsync(id, userUpdateDto);
        return Ok(userDto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = AppRole.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserAsync(string id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }
}