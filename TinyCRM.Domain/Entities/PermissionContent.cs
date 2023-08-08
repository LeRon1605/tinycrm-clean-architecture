namespace TinyCRM.Domain.Entities;

public class PermissionContent : Entity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<PermissionGrant> PermissionGrants { get; set; }

    public PermissionContent()
    {
    }

    public PermissionContent(string name, string description)
    {
        Name = name;
        Description = description;
    }
}