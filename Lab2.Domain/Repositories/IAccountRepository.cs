using Lab2.Domain.Entities;
using Lab2.Domain.Repositories.Interfaces;

namespace Lab2.Domain.Repositories;

public interface IAccountRepository : IRepository<Account, int>
{
    Task<bool> IsEmailExistingAsync(string email);

    Task<bool> IsPhoneExistingAsync(string phone);
}