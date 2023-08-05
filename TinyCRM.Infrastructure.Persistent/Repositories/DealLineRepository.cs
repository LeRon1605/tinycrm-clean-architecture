using TinyCRM.Application.Repositories;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.Persistent.Repositories.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories;

public class DealLineRepository : Repository<DealLine, int>, IDealLineRepository
{
    public DealLineRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}