using Lab2.API.Dtos;
using Lab2.Domain.Base;
using System.Linq.Expressions;

namespace Lab2.API.Extensions;

public static class RepositoryExtension
{
    public static async Task<PagedResultDto<TEntity>> GetPagedResultAsync<TEntity, TKey>(this IRepository<TEntity, TKey> repository, int skip, int take, Expression<Func<TEntity, bool>> expression, string? sorting = null, bool tracking = true, string? includeProps = null) where TEntity : IEntity<TKey>
    {
        var data = await repository.GetPagedListAsync(
                                                skip,
                                                take,
                                                expression,
                                                sorting,
                                                tracking,
                                                includeProps);
        var total = await repository.GetCountAsync(expression);

        return new PagedResultDto<TEntity>()
        {
            Data = data,
            TotalPages = (int)Math.Ceiling(total * 1.0 / take)
        };
    }
}