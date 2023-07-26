using System.ComponentModel.DataAnnotations;

namespace Lab2.API.Dtos;

public class UserCreateDto
{
    [Required(ErrorMessage = "Name is required")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}