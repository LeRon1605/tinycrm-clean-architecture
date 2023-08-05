using System.ComponentModel.DataAnnotations;
using TinyCRM.Domain.Common.Enums;

namespace TinyCRM.Application.Dtos.Leads;

public class LeadUpdateDto
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
    public LeadStatus Status { get; set; }

    [Required]
    public int CustomerId { get; set; }
}