using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Domain.Specifications;
using Lab2.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Infrastructure.Repositories;

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

    public Task<int> GetCountOpenDealAsync()
    {
        return GetCountAsync(new OpenDealSpecification());
    }

    public Task<int> GetCountWonDealAsync()
    {
        return GetCountAsync(new WonDealSpecification());
    }
}