using TinyCRM.Application.Dtos.Auth;

namespace TinyCRM.Application.Services.Abstracts;

public interface IAuthService
{
    Task<AuthCredentialDto> SignInAsync(LoginDto loginDto);
}