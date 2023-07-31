using Lab2.Domain.Base;
using Lab2.Domain.Entities;

namespace Lab2.Domain.Repositories;

public interface IAccountRepository : IRepository<Account>
{
    Task<bool> IsEmailExistingAsync(string email);
    Task<bool> IsPhoneExistingAsync(string phone);
}