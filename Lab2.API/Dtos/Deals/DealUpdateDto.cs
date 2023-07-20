using Lab2.Domain.Shared.Enums;

namespace Lab2.API.Dtos;

public class DealUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int EstimatedRevenue { get; set; }
    public DealStatus Status { get; set; }
}