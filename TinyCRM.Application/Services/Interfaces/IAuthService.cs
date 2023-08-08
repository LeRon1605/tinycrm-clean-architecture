using TinyCRM.Application.Dtos.Auth;

namespace TinyCRM.Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthCredentialDto> SignInAsync(LoginDto loginDto);

    Task<IEnumerable<string>> GetPermissionsForUserAsync(string userId);
}