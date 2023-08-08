using TinyCRM.Infrastructure.Persistent.Repositories.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories;

public class ProductRepository : Repository<Product, int>, IProductRepository
{
    public ProductRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public Task<bool> IsCodeExistingAsync(string code)
    {
        return AnyAsync(x => x.Code == code);
    }
}