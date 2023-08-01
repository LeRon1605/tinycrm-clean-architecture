using Lab2.Domain.Specifications.Deals;

namespace Lab2.Domain.Specifications;

public class DealSpecification
{
    public static readonly OpenDealSpecification OpenSpecification = new();
    public static readonly WonDealSpecification WonSpecification = new();
    public static readonly LostDealSpecification LostSpecification = new();
    public static readonly ProcessedDealSpecification ProcessedSpecification = new();
}