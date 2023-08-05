namespace TinyCRM.Domain.Specifications.Deals;

public class DealSpecification
{
    public static readonly OpenDealSpecification OpenSpecification = new();
    public static readonly WonDealSpecification WonSpecification = new();
    public static readonly LostDealSpecification LostSpecification = new();
    public static readonly ProcessedDealSpecification ProcessedSpecification = new();
}