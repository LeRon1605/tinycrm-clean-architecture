using Lab2.Domain.Entities;
using Lab2.Domain.Repositories.Interfaces;

namespace Lab2.Domain.Repositories;

public interface IUserRepository : IRepository<User, string>
{
    Task<User?> FindByUserNameOrEmailAsync(string username, string email);
}