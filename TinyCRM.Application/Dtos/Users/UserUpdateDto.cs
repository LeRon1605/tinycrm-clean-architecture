using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.Dtos.Users;

public class UserUpdateDto
{
    [Required]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}