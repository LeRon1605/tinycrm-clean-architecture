using TinyCRM.Application.Repositories.Base;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Repositories;

public interface IAccountRepository : IRepository<Account, int>
{
    Task<bool> IsEmailExistingAsync(string email);

    Task<bool> IsPhoneExistingAsync(string phone);
}