namespace TinyCRM.Application.Dtos.Deals;

public class DealStatisticDto
{
    public int OpenDeal { get; set; }
    public int WonDeal { get; set; }
    public double AverageRevenue { get; set; }
    public int TotalRevenue { get; set; }
}