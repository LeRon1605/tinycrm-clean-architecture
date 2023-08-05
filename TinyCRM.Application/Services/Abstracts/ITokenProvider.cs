using System.Security.Claims;

namespace TinyCRM.Application.Services.Abstracts;

public interface ITokenProvider
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
}