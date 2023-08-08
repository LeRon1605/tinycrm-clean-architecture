using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Infrastructure.Persistent.Repositories.Base;

namespace TinyCRM.Infrastructure.Persistent.Repositories;

public class PermissionRepository : Repository<PermissionContent, int>, IPermissionRepository
{
    public PermissionRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }

    public Task<PermissionContent> FindByNameAsync(string name)
    {
        return DbSet.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<IList<PermissionContent>> GetNotGrantedForAsync(ISpecification<PermissionGrant, int> specification)
    {
        var permissionQueryable = _dbContextFactory.DbContext
                                                   .Set<PermissionGrant>()
                                                   .Where(specification.ToExpression());

        return await DbSet.Where(permission => permissionQueryable.All(x => x.PermissionId != permission.Id))
                          .ToListAsync();
    }

    public async Task<IList<PermissionContent>> GetGrantedForAsync(ISpecification<PermissionGrant, int> specification)
    {
        return await _dbContextFactory.DbContext.Set<PermissionGrant>()
                                                .Include(x => x.Permission)
                                                .Where(specification.ToExpression())
                                                .Select(x => x.Permission)
                                                .ToListAsync();
    }
}