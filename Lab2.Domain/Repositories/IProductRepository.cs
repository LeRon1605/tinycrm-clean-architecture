using Lab2.Domain.Entities;
using Lab2.Domain.Repositories.Interfaces;

namespace Lab2.Domain.Repositories;

public interface IProductRepository : IRepository<Product, int>
{
    Task<bool> IsCodeExistingAsync(string code);
}