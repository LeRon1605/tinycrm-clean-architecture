using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.Dtos.Accounts;

public class AccountUpdateDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string Address { get; set; }
}