﻿using AutoMapper;
using Lab2.API.Dtos;
using Lab2.API.Exceptions;
using Lab2.Domain.Base;
using System.Linq.Expressions;

namespace Lab2.API.Services;

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
        return GetPagedAsync(skip: (filterParam.Page - 1) * filterParam.Size,
                             take: filterParam.Size,
                             expression: filterParam.ToExpression(),
                             sorting: filterParam.BuildSortingParam());
    }

    protected async Task<PagedResultDto<TEntityDto>> GetPagedAsync(int skip, int take, Expression<Func<TEntity, bool>> expression, string sorting)
    {
        var data = await _repository.GetPagedListAsync(
                                                skip: skip,
                                                take: take,
                                                expression,
                                                sorting,
                                                tracking: false,
                                                includeProps: _includePropsOnGet);
        var total = await _repository.GetCountAsync(expression);

        return new PagedResultDto<TEntityDto>()
        {
            Data = _mapper.Map<IEnumerable<TEntityDto>>(data),
            TotalPages = (int)Math.Ceiling(total * 1.0 / take)
        };
    }

    public virtual async Task<TEntityDto> GetAsync(TKey id)
    {
        TEntity? entity = await _repository.FindAsync(x => x.Id.Equals(id), includeProps: _includePropsOnGet, tracking: false);
        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString());
        }

        return _mapper.Map<TEntityDto>(entity);
    }

    public virtual async Task DeleteAsync(TKey id)
    {
        // Find entity by Id
        TEntity? entity = await _repository.FindByIdAsync(id);
        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString());
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
}

public class BaseService<TEntity, TKey, TEntityDto, TEntityCreateDto> : BaseService<TEntity, TKey, TEntityDto>, IService<TEntity, TKey, TEntityDto, TEntityCreateDto> where TEntity : IEntity<TKey>
{
    public BaseService(
        IMapper mapper,
        IRepository<TEntity, TKey> repository,
        IUnitOfWork unitOfWork) : base(mapper, repository, unitOfWork)
    {
    }

    public virtual async Task<TEntityDto> CreateAsync(TEntityCreateDto entityCreateDto)
    {
        // Perform business check before insert
        if (await IsValidOnInsertAsync(entityCreateDto))
        {
            // Create entity from dto
            var entity = _mapper.Map<TEntity>(entityCreateDto);

            // Insert entity to db
            _repository.Insert(entity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<TEntityDto>(entity);
        }

        throw new BadRequestException($"Invalid operation, can not insert {typeof(TEntity).Name}!");
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
            throw new EntityNotFoundException(typeof(TEntity).Name, id.ToString());
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

        throw new BadRequestException($"Invalid operation, can not update {typeof(TEntity).Name}!");
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