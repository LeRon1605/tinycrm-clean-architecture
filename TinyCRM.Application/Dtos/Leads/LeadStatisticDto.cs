namespace TinyCRM.Application.Dtos.Leads;

public class LeadStatisticDto
{
    public int OpenLead { get; set; }
    public int QualifiedLead { get; set; }
    public int DisqualifiedLead { get; set; }
    public decimal AverageEstimatedRevenue { get; set; }
}