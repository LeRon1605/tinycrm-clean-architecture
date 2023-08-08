using TinyCRM.Infrastructure.Persistent.Repositories.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories;

public class DealLineRepository : Repository<DealLine, int>, IDealLineRepository
{
    public DealLineRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}