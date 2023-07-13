using Lab2.Domain.Entities;
using Lab2.Domain.Shared.Enums;

namespace Lab2.API.Dtos;

public class DealDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int EstimatedRevenue { get; set; }
    public int ActualRevenue { get; set; }
    public DealStatus Status { get; set; }

    public int CustomerId { get; set; }
    public int? LeadId { get; set; }
}
