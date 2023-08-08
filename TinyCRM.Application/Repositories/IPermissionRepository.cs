using TinyCRM.Domain.Specifications.Abstracts;

namespace TinyCRM.Application.Repositories;

public interface IPermissionRepository : IRepository<PermissionContent, int>
{
    Task<PermissionContent> FindByNameAsync(string name);

    Task<IList<PermissionContent>> GetNotGrantedForAsync(ISpecification<PermissionGrant, int> specification);

    Task<IList<PermissionContent>> GetGrantedForAsync(ISpecification<PermissionGrant, int> specification);
}