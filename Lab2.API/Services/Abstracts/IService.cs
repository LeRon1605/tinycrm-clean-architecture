using Lab2.API.Dtos;
using Lab2.Domain.Base;

namespace Lab2.API.Services;

public interface IService<TEntity, TKey, TEntityDto> where TEntity : IEntity<TKey>
{
    Task<PagedResultDto<TEntityDto>> GetPagedAsync(IFilterDto<TEntity, TKey> filterParam);

    Task<TEntityDto> GetAsync(TKey id);

    Task DeleteAsync(TKey id);
}

public interface IService<TEntity, TKey, TEntityDto, TEntityCreateDto> : IService<TEntity, TKey, TEntityDto> where TEntity : IEntity<TKey>
{
    Task<TEntityDto> CreateAsync(TEntityCreateDto entityCreateDto);
}

public interface IService<TEntity, TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IService<TEntity, TKey, TEntityDto, TEntityCreateDto> where TEntity : IEntity<TKey>
{
    Task<TEntityDto> UpdateAsync(TKey id, TEntityUpdateDto entityUpdateDto);
}