using Microsoft.AspNetCore.Identity;
using TinyCRM.Domain.Entities.Base;

namespace TinyCRM.Infrastructure.Identity;

public class ApplicationUser : IdentityUser, IEntity<string>
{
    public string FullName { get; set; }
}