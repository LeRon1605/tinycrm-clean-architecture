using Lab2.Domain.Base;

namespace Lab2.Domain.Specifications;

public interface ISortingSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : IEntity<TKey>
{
    string Sorting { get; }
    string BuildSorting(string sorting);
}