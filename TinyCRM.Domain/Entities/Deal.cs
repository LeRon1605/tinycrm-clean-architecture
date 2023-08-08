using TinyCRM.Domain.Common.Enums;

namespace TinyCRM.Domain.Entities;

public class Deal : Entity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int EstimatedRevenue { get; set; }
    public DealStatus Status { get; set; }

    public int LeadId { get; set; }
    public Lead Lead { get; set; }

    public ICollection<DealLine> Lines { get; set; }

    public Deal()
    {
    }

    public Deal(Lead lead)
    {
        Title = lead.Title;
        EstimatedRevenue = lead.EstimatedRevenue;
        Description = string.Empty;
        Status = DealStatus.Open;
        LeadId = lead.Id;
    }
}