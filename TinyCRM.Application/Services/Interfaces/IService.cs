namespace TinyCRM.Application.Services.Interfaces;

public interface IService<TEntity, TKey, TEntityDto> where TEntity : IEntity<TKey>
{
    Task<PagedResultDto<TEntityDto>> GetPagedAsync(IFilterDto<TEntity, TKey> filterParam);

    Task<TEntityDto> GetAsync(TKey id);
}

public interface IService<TEntity, TKey, TEntityDto, in TEntityCreateDto> : IService<TEntity, TKey, TEntityDto> where TEntity : IEntity<TKey>
{
    Task DeleteAsync(TKey id);

    Task<TEntityDto> CreateAsync(TEntityCreateDto entityCreateDto);
}

public interface IService<TEntity, TKey, TEntityDto, in TEntityCreateDto, in TEntityUpdateDto> : IService<TEntity, TKey, TEntityDto, TEntityCreateDto> where TEntity : IEntity<TKey>
{
    Task<TEntityDto> UpdateAsync(TKey id, TEntityUpdateDto entityUpdateDto);
}