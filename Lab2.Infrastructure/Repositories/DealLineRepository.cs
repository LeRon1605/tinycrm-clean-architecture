using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;

namespace Lab2.Infrastructure.Repositories;

public class DealLineRepository : Repository<DealLine>, IDealLineRepository
{
    public DealLineRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}