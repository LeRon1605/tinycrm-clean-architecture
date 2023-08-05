using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.Dtos.Contacts;

public class ContactUpdateDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    public int? AccountId { get; set; }
}