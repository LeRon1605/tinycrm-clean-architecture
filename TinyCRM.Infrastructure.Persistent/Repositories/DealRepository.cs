using TinyCRM.Domain.Specifications.Deals;
using TinyCRM.Infrastructure.Persistent.Repositories.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories;

public class DealRepository : Repository<Deal, int>, IDealRepository
{
    public DealRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public Task<double> GetAverageRevenueAsync()
    {
        return DbSet.Include(x => x.Lines).SelectMany(x => x.Lines).AverageAsync(x => x.Quantity * x.PricePerUnit);
    }

    public Task<int> GetTotalRevenueAsync()
    {
        return DbSet.Include(x => x.Lines).SelectMany(x => x.Lines).SumAsync(x => x.Quantity * x.PricePerUnit);
    }
}