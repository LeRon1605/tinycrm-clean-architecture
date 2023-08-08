using TinyCRM.Domain.Entities.Base;

namespace TinyCRM.Infrastructure.Identity;

public class ApplicationRole : IdentityRole, IEntity<string>
{
    public ApplicationRole(string name) : base(name)
    {
    }
}