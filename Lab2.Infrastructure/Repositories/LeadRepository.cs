using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;

namespace Lab2.Infrastructure.Repositories;

public class LeadRepository : Repository<Lead>, ILeadRepository
{
    public LeadRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}