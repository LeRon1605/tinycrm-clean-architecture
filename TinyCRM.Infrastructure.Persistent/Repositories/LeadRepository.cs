using TinyCRM.Application.Repositories;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Specifications.Leads;
using TinyCRM.Infrastructure.Persistent.Repositories.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories;

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