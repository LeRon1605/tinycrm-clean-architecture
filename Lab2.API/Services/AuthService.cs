using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Lab2.API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenProvider _tokenProvider;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenProvider tokenProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthCredentialDto> SignInAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new NotFoundException("Account with provided information does not exist!", ErrorCodes.IncorrectAccountInfo);
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, true);
        if (signInResult.Succeeded)
        {
            return new AuthCredentialDto()
            {
                AccessToken = _tokenProvider.GenerateAccessToken(await GetUserAuthenticateClaimsAsync(user))
            };
        }

        if (signInResult.RequiresTwoFactor)
        {
            throw new NotImplementException("Two factor authentication is not currently implemented!",
                ErrorCodes.NotImplementTwoFactor);
        }

        if (signInResult.IsLockedOut)
        {
            throw new BadRequestException("This account has been locked out!", ErrorCodes.AccountLockedOut);
        }

        throw new BadRequestException("Account with provided information does not exist!", ErrorCodes.IncorrectAccountInfo);
    }

    private async Task<IEnumerable<Claim>> GetUserAuthenticateClaimsAsync(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        if (_userManager.SupportsUserClaim)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            if (userClaims != null)
            {
                claims.AddRange(userClaims);
            }
        }

        return claims;
    }
}