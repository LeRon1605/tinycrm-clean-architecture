using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lab2.Infrastructure.Repositories;

public class AccountRepository : Repository<Account, int>, IAccountRepository
{
    public AccountRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public Task<Account> FindDetailAsync(Expression<Func<Account, bool>> expression)
    {
        return DbSet.Include(x => x.Contacts)
                    .Include(x => x.Leads)
                    .FirstOrDefaultAsync(expression);
    }
}
