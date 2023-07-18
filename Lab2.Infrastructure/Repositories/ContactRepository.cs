using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Infrastructure.Repositories;

public class ContactRepository : Repository<Contact>, IContactRepository
{
    public ContactRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}