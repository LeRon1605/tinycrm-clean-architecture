using Lab2.API.Dtos;

namespace Lab2.API.Services;

public interface IAuthService
{
    Task<AuthCredentialDto> SignInAsync(LoginDto loginDto);
}