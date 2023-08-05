using TinyCRM.Domain.Common.Enums;

namespace TinyCRM.Application.Dtos.Deals;

public class DealDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int EstimatedRevenue { get; set; }
    public int ActualRevenue { get; set; }
    public DealStatus Status { get; set; }

    public int? LeadId { get; set; }
}