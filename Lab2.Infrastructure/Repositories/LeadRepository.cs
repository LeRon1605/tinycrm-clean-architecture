using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Domain.Specifications;
using Lab2.Domain.Specifications.Leads;
using Lab2.Infrastructure.Base;

namespace Lab2.Infrastructure.Repositories;

public class LeadRepository : Repository<Lead, int>, ILeadRepository
{
    public LeadRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public Task<int> GetCountOpenLeadAsync()
    {
        return GetCountAsync(new OpenLeadSpecification());
    }

    public Task<int> GetCountDisqualifiedAsync()
    {
        return GetCountAsync(new DisqualifiedLeadSpecification());
    }

    public Task<int> GetCountQualifiedAsync()
    {
        return GetCountAsync(new QualifiedLeadSpecification());
    }

    public Task<decimal> GetAverageEstimatedRevenueAsync()
    {
        return GetAverageAsync(x => x.EstimatedRevenue);
    }
}