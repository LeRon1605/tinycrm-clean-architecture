using TinyCRM.Application.Repositories.Base;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Repositories;

public interface IDealRepository : IRepository<Deal, int>
{
    Task<double> GetAverageRevenueAsync();

    Task<int> GetTotalRevenueAsync();

    Task<int> GetCountOpenDealAsync();

    Task<int> GetCountWonDealAsync();
}