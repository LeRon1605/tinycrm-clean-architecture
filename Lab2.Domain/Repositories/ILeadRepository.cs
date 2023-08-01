using Lab2.Domain.Base;
using Lab2.Domain.Entities;

namespace Lab2.Domain.Repositories;

public interface ILeadRepository : IRepository<Lead>
{
    Task<int> GetCountOpenLeadAsync();

    Task<int> GetCountDisqualifiedAsync();

    Task<int> GetCountQualifiedAsync();

    Task<decimal> GetAverageEstimatedRevenueAsync();
}