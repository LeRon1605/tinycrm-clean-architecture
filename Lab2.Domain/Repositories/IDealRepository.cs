using Lab2.Domain.Entities;
using Lab2.Domain.Repositories.Interfaces;

namespace Lab2.Domain.Repositories;

public interface IDealRepository : IRepository<Deal, int>
{
    Task<double> GetAverageRevenueAsync();

    Task<int> GetTotalRevenueAsync();

    Task<int> GetCountOpenDealAsync();

    Task<int> GetCountWonDealAsync();
}