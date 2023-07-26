using Lab2.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Lab2.Domain.Entities;

public class User : IdentityUser, IEntity<string>
{
    public string FullName { get; set; }
}