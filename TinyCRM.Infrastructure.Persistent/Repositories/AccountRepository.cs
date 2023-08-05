using TinyCRM.Application.Repositories;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.Persistent.Repositories.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories;

public class AccountRepository : Repository<Account, int>, IAccountRepository
{
    public AccountRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public Task<bool> IsEmailExistingAsync(string email)
    {
        return AnyAsync(x => x.Email == email);
    }

    public Task<bool> IsPhoneExistingAsync(string phone)
    {
        return AnyAsync(x => x.Phone == phone);
    }
}