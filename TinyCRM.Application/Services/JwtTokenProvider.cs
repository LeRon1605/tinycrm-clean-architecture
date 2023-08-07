using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Services.Abstracts;

namespace TinyCRM.Application.Services;

public class JwtTokenProvider : ITokenProvider
{
    private readonly JwtSetting _jwtSetting;

    public JwtTokenProvider(IOptions<JwtSetting> jwtOptions)
    {
        _jwtSetting = jwtOptions.Value;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = Encoding.UTF8.GetBytes(_jwtSetting.Key);
        var token = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSetting.Expires),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}