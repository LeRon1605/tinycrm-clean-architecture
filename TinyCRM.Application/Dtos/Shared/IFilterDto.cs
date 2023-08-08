using TinyCRM.Domain.Specifications.Abstracts;

namespace TinyCRM.Application.Dtos.Shared;

public interface IFilterDto<TEntity, TKey> where TEntity : IEntity<TKey>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string Sorting { get; set; }

    IPagingAndSortingSpecification<TEntity, TKey> ToSpecification();
}

public interface IFilterDto<TEntity> : IFilterDto<TEntity, int> where TEntity : IEntity<int>
{
}