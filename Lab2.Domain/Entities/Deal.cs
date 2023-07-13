using Lab2.Domain.Shared.Enums;

namespace Lab2.Domain.Entities;

public class Deal : IEntity<int>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int EstimatedRevenue { get; set; }
    public DealStatus Status { get; set; }

    public int CustomerId { get; set; }
    public Account Customer { get; set; }

    public int? LeadId { get; set; }
    public Lead? Lead { get; set; }

    public ICollection<DealLine> Lines { get; set; }
}
