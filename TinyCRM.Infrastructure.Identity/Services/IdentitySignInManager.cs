using System.Security.Claims;
using TinyCRM.Application.Repositories.Base;
using TinyCRM.Infrastructure.Identity.Exceptions;

namespace TinyCRM.Infrastructure.Identity.Services;

public class IdentitySignInManager : ISignInManager
{
    private readonly IReadOnlyRepository<ApplicationUser, string> _userRepository;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentitySignInManager(
        IReadOnlyRepository<ApplicationUser, string> userRepository,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _signInManager = signInManager;
    }

    public async Task<IEnumerable<Claim>> SignInAsync(string userNameOrEmail, string password)
    {
        var user = await _userRepository.FindAsync(x => x.UserName == userNameOrEmail || x.Email == userNameOrEmail);
        if (user == null)
        {
            throw new InvalidCredentialException();
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, true);
        if (signInResult.Succeeded)
        {
            return await GetUserAuthenticateClaimsAsync(user);
        }

        if (signInResult.IsLockedOut)
        {
            throw new AccountLockedOutException();
        }

        throw new InvalidCredentialException();
    }

    private async Task<IEnumerable<Claim>> GetUserAuthenticateClaimsAsync(ApplicationUser user)
    {
        var claims = new List<Claim>()
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Name, user.UserName),
            new (ClaimTypes.Email, user.Email)
        };

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}