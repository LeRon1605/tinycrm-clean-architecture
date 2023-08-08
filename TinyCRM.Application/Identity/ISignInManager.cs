using System.Security.Claims;

namespace TinyCRM.Application.Identity;

public interface ISignInManager
{
    Task<IEnumerable<Claim>> SignInAsync(string userNameOrEmail, string password);
}