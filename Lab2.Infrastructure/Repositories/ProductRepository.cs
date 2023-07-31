using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;

namespace Lab2.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public Task<bool> IsCodeExistingAsync(string code)
    {
        return AnyAsync(x => x.Code == code);
    }
}