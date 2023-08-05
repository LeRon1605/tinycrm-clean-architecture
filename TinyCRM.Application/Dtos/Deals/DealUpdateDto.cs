using System.ComponentModel.DataAnnotations;
using TinyCRM.Domain.Common.Enums;

namespace TinyCRM.Application.Dtos.Deals;

public class DealUpdateDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int EstimatedRevenue { get; set; }

    [Required]
    public DealStatus Status { get; set; }
}