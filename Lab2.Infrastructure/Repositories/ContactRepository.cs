using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lab2.Infrastructure.Repositories;

public class ContactRepository : Repository<Contact, int>, IContactRepository
{
    public ContactRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public Task<Contact> FindDetailAsync(Expression<Func<Contact, bool>> expression)
    {
        return DbSet.Include(x => x.Account).FirstOrDefaultAsync(expression);
    }
}
