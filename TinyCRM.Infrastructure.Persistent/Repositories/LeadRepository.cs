using TinyCRM.Domain.Specifications.Leads;
using TinyCRM.Infrastructure.Persistent.Repositories.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories;

public class LeadRepository : Repository<Lead, int>, ILeadRepository
{
    public LeadRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public Task<decimal> GetAverageEstimatedRevenueAsync()
    {
        return GetAverageAsync(x => x.EstimatedRevenue);
    }
}