﻿using TinyCRM.Application.Repositories.Base;
using TinyCRM.Domain.Entities.Base;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Infrastructure.Persistent.Commons;

namespace TinyCRM.Infrastructure.Persistent.Repositories.Base;

public class SpecificationRepository<TEntity, TKey> : ISpecificationRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    private readonly Lazy<DbSet<TEntity>> _dbSet;
    protected DbSet<TEntity> DbSet => _dbSet.Value;
    protected DbContextFactory _dbContextFactory { get; }

    public SpecificationRepository(DbContextFactory dbContextFactory)
    {
        _dbSet = new Lazy<DbSet<TEntity>>(dbContextFactory.DbContext.Set<TEntity>());
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IList<TEntity>> GetPagedListAsync(IPagingAndSortingSpecification<TEntity, TKey> specification)
    {
        return await SpecificationEvaluator<TEntity, TKey>.GetQuery(DbSet.AsQueryable(), specification).ToListAsync();
    }

    public async Task<IList<TEntity>> GetPagedListAsync(int skip, int take, ISpecification<TEntity, TKey> specification, string? sorting = null)
    {
        return await SpecificationEvaluator<TEntity, TKey>.GetQuery(DbSet.AsQueryable(), specification)
                                                            .Skip(skip).Take(take).ToListAsync();
    }

    public Task<TEntity?> FindAsync(ISpecification<TEntity, TKey> specification)
    {
        return SpecificationEvaluator<TEntity, TKey>.GetQuery(DbSet.AsQueryable(), specification).FirstOrDefaultAsync();
    }

    public Task<int> GetCountAsync(ISpecification<TEntity, TKey> specification)
    {
        return DbSet.CountAsync(specification.ToExpression());
    }

    public Task<bool> AnyAsync(ISpecification<TEntity, TKey> specification)
    {
        return DbSet.AnyAsync(specification.ToExpression());
    }
}