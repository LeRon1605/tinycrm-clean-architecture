using TinyCRM.Application.Repositories;
using TinyCRM.Domain.Entities;
using TinyCRM.Infrastructure.Persistent.Repositories.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories;

public class ContactRepository : Repository<Contact, int>, IContactRepository
{
    public ContactRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}