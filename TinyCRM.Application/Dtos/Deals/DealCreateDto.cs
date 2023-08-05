using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.Dtos.Deals;

public class DealCreateDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int EstimatedRevenue { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int? LeadId { get; set; }
}