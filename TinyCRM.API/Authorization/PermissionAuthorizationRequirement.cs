namespace TinyCRM.API.Authorization;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public string Permission { get; set; } = string.Empty;
}