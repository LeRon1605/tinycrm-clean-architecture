namespace TinyCRM.Application.Repositories;

public interface ILeadRepository : IRepository<Lead, int>
{
    Task<int> GetCountOpenLeadAsync();

    Task<int> GetCountDisqualifiedAsync();

    Task<int> GetCountQualifiedAsync();

    Task<decimal> GetAverageEstimatedRevenueAsync();
}