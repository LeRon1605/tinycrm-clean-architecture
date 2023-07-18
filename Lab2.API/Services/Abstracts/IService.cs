using Lab2.API.Dtos;
using Lab2.Domain.Base;

namespace Lab2.API.Services;

public interface IService<TEntity> where TEntity : Entity
{

}

public interface IService<TEntity, TEntityDto> : IService<TEntity> where TEntity : Entity
{
    Task<PagedResultDto<TEntityDto>> GetPagedAsync(IFilterDto<TEntity> filterParam);
    Task<TEntityDto> GetAsync(int id);
    Task DeleteAsync(int id);
}

public interface IService<TEntity, TEntityDto, TEntityCreateDto> : IService<TEntity, TEntityDto> where TEntity : Entity
{
    Task<TEntityDto> CreateAsync(TEntityCreateDto entityCreateDto);
}

public interface IService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IService<TEntity, TEntityDto, TEntityCreateDto> where TEntity : Entity
{
    Task<TEntityDto> UpdateAsync(int id, TEntityUpdateDto entityUpdateDto);
}