namespace TinyCRM.Application.Dtos.Shared;

public class JwtSetting
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string Key { get; set; }
    public int Expires { get; set; }
}