using Lab2.Domain.Entities;
using Lab2.Domain.Repositories.Interfaces;

namespace Lab2.Domain.Repositories;

public interface ILeadRepository : IRepository<Lead, int>
{
    Task<int> GetCountOpenLeadAsync();

    Task<int> GetCountDisqualifiedAsync();

    Task<int> GetCountQualifiedAsync();

    Task<decimal> GetAverageEstimatedRevenueAsync();
}