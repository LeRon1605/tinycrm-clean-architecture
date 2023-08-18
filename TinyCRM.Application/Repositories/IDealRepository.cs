namespace TinyCRM.Application.Repositories;

public interface IDealRepository : IRepository<Deal, int>
{
    Task<double> GetAverageRevenueAsync();

    Task<int> GetTotalRevenueAsync();
}