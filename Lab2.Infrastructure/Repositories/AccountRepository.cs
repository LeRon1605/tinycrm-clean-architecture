using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;

namespace Lab2.Infrastructure.Repositories;

public class AccountRepository : Repository<Account, int>, IAccountRepository
{
    public AccountRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}
