using Lab2.Domain.Base;
using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.Domain.Repositories;

public interface IAccountRepository : IRepository<Account, int>
{
    Task<Account> FindDetailAsync(Expression<Func<Account, bool>> expression);
}
