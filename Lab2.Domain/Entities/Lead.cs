using Lab2.Domain.Base;
using Lab2.Domain.Shared.Enums;

namespace Lab2.Domain.Entities;

public class Lead : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public int EstimatedRevenue { get; set; }
    public LeadStatus Status { get; set; }

    public int CustomerId { get; set; }
    public Account Customer { get; set; }

    public Deal? Deal { get; set; }

    public string? Reason { get; set; }
    public string? ReasonDescription { get; set; }
    public DateTime? EndedDate { get; set; }
}
