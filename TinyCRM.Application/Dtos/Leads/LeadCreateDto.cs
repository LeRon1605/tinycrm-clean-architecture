using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.Dtos.Leads;

public class LeadCreateDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Source { get; set; }

    [Required]
    public int EstimatedRevenue { get; set; }

    [Required]
    public int CustomerId { get; set; }
}