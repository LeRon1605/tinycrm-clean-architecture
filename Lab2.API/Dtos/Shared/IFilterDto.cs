using Lab2.Domain.Base;
using System.Linq.Expressions;

namespace Lab2.API.Dtos;

public interface IFilterDto<TEntity, TKey> where TEntity : IEntity<TKey>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string Sorting { get; set; }

    Expression<Func<TEntity, bool>> ToExpression();

    string BuildSortingParam();
}

public interface IFilterDto<TEntity> : IFilterDto<TEntity, int> where TEntity : IEntity<int>
{
}