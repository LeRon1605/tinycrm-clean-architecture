using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;

namespace Lab2.Infrastructure.Repositories;

public class DealRepository : Repository<Deal, int>, IDealRepository
{
    public DealRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}
