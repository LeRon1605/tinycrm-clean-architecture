using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;

namespace Lab2.Infrastructure.Repositories;

public class ContactRepository : Repository<Contact, int>, IContactRepository
{
    public ContactRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}