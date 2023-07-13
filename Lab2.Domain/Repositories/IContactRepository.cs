using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.Domain.Repositories;

public interface IContactRepository : IRepository<Contact, int>
{
    Task<Contact> FindDetailAsync(Expression<Func<Contact, bool>> expression);
}
