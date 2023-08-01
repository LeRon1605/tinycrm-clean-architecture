namespace Lab2.Domain.Specifications;

public class DealSpecification
{
    public static readonly OpenDealSpecification OpenSpecification = new();
    public static readonly WonDealSpecification WonDealSpecification = new();
    public static readonly LostDealSpecification LostDealSpecification = new();
}