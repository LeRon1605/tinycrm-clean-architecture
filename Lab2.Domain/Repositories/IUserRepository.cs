using Lab2.Domain.Base;
using Lab2.Domain.Entities;

namespace Lab2.Domain.Repositories;

public interface IUserRepository : IRepository<User, string>
{
    Task<User> FindByUserNameOrEmailAsync(string username, string email);
}