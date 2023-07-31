﻿using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Lab2.API.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUserRepository _userRepository;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUserRepository userRepository,
        ITokenProvider tokenProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenProvider = tokenProvider;
        _userRepository = userRepository;
    }

    public async Task<AuthCredentialDto> SignInAsync(LoginDto loginDto)
    {
        var user = await _userRepository.FindByUserNameOrEmailAsync(loginDto.UserNameOrEmail, loginDto.UserNameOrEmail);
        if (user == null)
        {
            throw new InvalidCredentialException();
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
        if (signInResult.Succeeded)
        {
            var claims = await GetUserAuthenticateClaimsAsync(user);
            return new AuthCredentialDto()
            {
                AccessToken = _tokenProvider.GenerateAccessToken(claims)
            };
        }

        if (signInResult.RequiresTwoFactor)
        {
            throw new NotImplementException("Two factor authentication is not currently implemented!", ErrorCodes.NotImplementTwoFactor);
        }

        if (signInResult.IsLockedOut)
        {
            throw new AccountLockedOutException();
        }

        throw new InvalidCredentialException();
    }

    private async Task<IEnumerable<Claim>> GetUserAuthenticateClaimsAsync(User user)
    {
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.UserName),
            new (ClaimTypes.Email, user.Email)
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