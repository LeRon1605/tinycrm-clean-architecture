using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;

namespace Lab2.Infrastructure.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
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