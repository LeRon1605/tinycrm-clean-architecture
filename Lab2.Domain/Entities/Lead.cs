using Lab2.Domain.Shared.Enums;

namespace Lab2.Domain.Entities;

public class Lead : IEntity<int>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public int EstimatedRevenue { get; set; }
    public LeadStatus Status { get; set; }

    public int CustomerId { get; set; }
    public Account Customer { get; set; }
    public ICollection<Deal> Deals { get; set; }
}
