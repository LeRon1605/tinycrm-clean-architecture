using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.Dtos.Leads;

public class DisqualifiedLeadCreateDto
{
    [Required]
    public string Reason { get; set; }

    [Required]
    public string ReasonDescription { get; set; }
}