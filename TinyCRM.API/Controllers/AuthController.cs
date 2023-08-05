using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Dtos.Auth;
using TinyCRM.Application.Services.Abstracts;

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
        var authCredentialDto = await _authService.SignInAsync(loginDto);
        return Ok(authCredentialDto);
    }
}