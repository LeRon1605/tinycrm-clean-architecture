namespace TinyCRM.Application.Repositories;

public interface ILeadRepository : IRepository<Lead, int>
{
    Task<decimal> GetAverageEstimatedRevenueAsync();
}