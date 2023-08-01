using Lab2.Domain.Specifications.Leads;

namespace Lab2.Domain.Specifications;

public static class LeadSpecification
{
    public static readonly OpenLeadSpecification OpenSpecification = new();
    public static readonly QualifiedLeadSpecification QualifiedSpecification = new();
    public static readonly DisqualifiedLeadSpecification DisqualifiedSpecification = new();
    public static readonly ProcessedLeadSpecification ProcessedLeadSpecification = new();
}