using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lab2.Infrastructure.Repositories;

public class DealRepository : Repository<Deal, int>, IDealRepository
{
    public DealRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public override Task<List<Deal>> GetListAsync(int skip, int take, Expression<Func<Deal, bool>> expression)
    {
        return DbSet.Skip(skip).Take(take).Include(x => x.Lines).ThenInclude(x => x.Product).Where(expression).ToListAsync();
    }

    public override Task<Deal> FindAsync(Expression<Func<Deal, bool>> expression)
    {
        return DbSet.Include(x => x.Lines).ThenInclude(x => x.Product).FirstOrDefaultAsync(expression);
    }
}
