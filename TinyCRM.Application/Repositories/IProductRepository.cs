namespace TinyCRM.Application.Repositories;

public interface IProductRepository : IRepository<Product, int>
{
    Task<bool> IsCodeExistingAsync(string code);
}