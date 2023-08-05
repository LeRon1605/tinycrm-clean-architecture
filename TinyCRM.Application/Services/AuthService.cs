using TinyCRM.Application.Common.Identity;
using TinyCRM.Application.Dtos.Auth;
using TinyCRM.Application.Services.Abstracts;

namespace TinyCRM.Application.Services;

public class AuthService : IAuthService
{
    private readonly ISignInManager _signInManager;
    private readonly ITokenProvider _tokenProvider;

    public AuthService(ISignInManager signInManager, ITokenProvider tokenProvider)
    {
        _signInManager = signInManager;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthCredentialDto> SignInAsync(LoginDto loginDto)
    {
        var claims = await _signInManager.SignInAsync(loginDto.UserNameOrEmail, loginDto.Password);

        return new AuthCredentialDto()
        {
            AccessToken = _tokenProvider.GenerateAccessToken(claims)
        };
    }
}