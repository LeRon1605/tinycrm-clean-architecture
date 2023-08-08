using System.Security.Claims;

namespace TinyCRM.Application.Services.Interfaces;

public interface ITokenProvider
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
}