using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.Dtos.Auth;

public class LoginDto
{
    [Required]
    public string UserNameOrEmail { get; set; }

    [Required]
    public string Password { get; set; }
}