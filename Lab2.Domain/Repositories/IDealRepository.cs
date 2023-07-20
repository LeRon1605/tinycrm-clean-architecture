using Lab2.Domain.Base;
using Lab2.Domain.Entities;

namespace Lab2.Domain.Repositories;

public interface IDealRepository : IRepository<Deal>
{
    Task<double> GetAverageRevenueAsync();

    Task<int> GetTotalRevenueAsync();
}