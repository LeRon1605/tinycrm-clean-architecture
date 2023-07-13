namespace Lab2.API.Dtos;

public class LeadCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public int EstimatedRevenue { get; set; }
    public int CustomerId { get; set; }
}
