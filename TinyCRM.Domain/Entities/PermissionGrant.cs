using TinyCRM.Domain.Common.Enums;

namespace TinyCRM.Domain.Entities;

public class PermissionGrant : Entity<int>
{
    public PermissionProvider Provider { get; set; }
    public string ProviderKey { get; set; }
    public int PermissionId { get; set; }
    public PermissionContent Permission { get; set; }
}