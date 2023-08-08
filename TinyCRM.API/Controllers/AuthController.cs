using TinyCRM.Application.Dtos.Auth;

namespace TinyCRM.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        var authCredential = await _authService.SignInAsync(loginDto);
        return Ok(authCredential);
    }
}