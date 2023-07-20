namespace Lab2.API.Dtos;

public class DealCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int EstimatedRevenue { get; set; }

    public int CustomerId { get; set; }
    public int? LeadId { get; set; }
}