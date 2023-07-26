using System.Security.Claims;

namespace Lab2.API.Services;

public interface ITokenProvider
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
}