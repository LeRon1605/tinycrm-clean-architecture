using AutoMapper;
using TinyCRM.Application.Common.UnitOfWorks;
using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Repositories.Base;
using TinyCRM.Application.Services.Abstracts;
using TinyCRM.Domain.Entities.Base;
using TinyCRM.Domain.Exceptions.Resource;
using TinyCRM.Domain.Specifications.Abstracts;

namespace TinyCRM.Application.Services;

public class BaseService<TEntity, TKey, TEntityDto> : IService<TEntity, TKey, TEntityDto> where TEntity : IEntity<TKey>
{
    protected IRepository<TEntity, TKey> _repository;
    protected IMapper _mapper;
    protected IUnitOfWork _unitOfWork;
    protected string _includePropsOnGet;

    public BaseService(
        IMapper mapper,
        IRepository<TEntity, TKey> repository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _includePropsOnGet = string.Empty;
    }

    public virtual Task<PagedResultDto<TEntityDto>> GetPagedAsync(IFilterDto<TEntity, TKey> filterParam)
    {
        return GetPagedAsync(filterParam.ToSpecification());
    }

    protected async Task<PagedResultDto<TEntityDto>> GetPagedAsync(IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        var data = await _repository.GetPagedListAsync(specification);
        var total = await _repository.GetCountAsync(specification);

        return new PagedResultDto<TEntityDto>()
        {
            Data = _mapper.Map<IEnumerable<TEntityDto>>(data),
            TotalPages = (int)Math.Ceiling(total * 1.0 / specification.Take)
        };
    }

    public virtual async Task<TEntityDto> GetAsync(TKey id)
    {
        TEntity? entity = await _repository.FindByIdAsync(id, includeProps: _includePropsOnGet, tracking: false);
        if (entity == null)
        {
            throw new ResourceNotFoundException(typeof(TEntity).Name, id.ToString());
        }

        return _mapper.Map<TEntityDto>(entity);
    }

    protected async Task CheckExistingAsync(TKey id)
    {
        var isExisting = await _repository.IsExistingAsync(id);
        if (!isExisting)
        {
            throw new ResourceNotFoundException(typeof(TEntity).Name, id.ToString());
        }
    }
}

public class BaseService<TEntity, TKey, TEntityDto, TEntityCreateDto> : BaseService<TEntity, TKey, TEntityDto>, IService<TEntity, TKey, TEntityDto, TEntityCreateDto> where TEntity : IEntity<TKey>
{
    public BaseService(
        IMapper mapper,
        IRepository<TEntity, TKey> repository,
        IUnitOfWork unitOfWork) : base(mapper, repository, unitOfWork)
    {
    }

    public virtual async Task DeleteAsync(TKey id)
    {
        // Find entity by Id
        TEntity? entity = await _repository.FindByIdAsync(id);
        if (entity == null)
        {
            throw new ResourceNotFoundException(typeof(TEntity).Name, id.ToString());
        }

        // Perform business check before delete
        if (await IsValidOnDeleteAsync(entity))
        {
            _repository.Delete(entity);
            await _unitOfWork.CommitAsync();
        }
    }

    /// <summary>
    /// Perform business check before delete.
    /// </summary>
    protected virtual Task<bool> IsValidOnDeleteAsync(TEntity entity)
    {
        return Task.FromResult(true);
    }

    public virtual async Task<TEntityDto> CreateAsync(TEntityCreateDto entityCreateDto)
    {
        // Perform business check before insert
        if (await IsValidOnInsertAsync(entityCreateDto))
        {
            // Create entity from dto
            var entity = _mapper.Map<TEntity>(entityCreateDto);

            // Insert entity to db
            await _repository.InsertAsync(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<TEntityDto>(entity);
        }

        throw new ResourceInvalidOperationException($"Invalid operation, can not insert {typeof(TEntity).Name}!");
    }

    /// <summary>
    /// Perform business check before insert.
    /// </summary>
    protected virtual Task<bool> IsValidOnInsertAsync(TEntityCreateDto entityCreateDto)
    {
        return Task.FromResult(true);
    }
}

public class BaseService<TEntity, TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseService<TEntity, TKey, TEntityDto, TEntityCreateDto>, IService<TEntity, TKey, TEntityDto, TEntityCreateDto, TEntityUpdateDto> where TEntity : IEntity<TKey>
{
    public BaseService(
        IMapper mapper,
        IRepository<TEntity, TKey> repository,
        IUnitOfWork unitOfWork) : base(mapper, repository, unitOfWork)
    {
    }

    public virtual async Task<TEntityDto> UpdateAsync(TKey id, TEntityUpdateDto entityUpdateDto)
    {
        // Fetch entity from db
        var entity = await _repository.FindByIdAsync(id);
        if (entity == null)
        {
            throw new ResourceNotFoundException(typeof(TEntity).Name, id.ToString());
        }

        // Perform business check before update
        if (await IsValidOnUpdateAsync(entity, entityUpdateDto))
        {
            // Update entity
            entity = UpdateEntity(entity, entityUpdateDto);

            // Update to db
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<TEntityDto>(entity);
        }

        throw new ResourceInvalidOperationException($"Invalid operation, can not update {typeof(TEntity).Name}!");
    }

    /// <summary>
    /// Update entity instance.
    /// </summary>
    protected virtual TEntity UpdateEntity(TEntity entity, TEntityUpdateDto entityUpdateDto)
    {
        _mapper.Map(entityUpdateDto, entity);
        return entity;
    }

    /// <summary>
    /// Perform business check before update.
    /// </summary>
    protected virtual Task<bool> IsValidOnUpdateAsync(TEntity entity, TEntityUpdateDto entityUpdateDto)
    {
        return Task.FromResult(true);
    }
}