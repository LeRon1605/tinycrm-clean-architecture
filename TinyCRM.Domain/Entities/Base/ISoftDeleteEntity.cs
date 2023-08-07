namespace TinyCRM.Domain.Entities.Base;

public interface ISoftDeleteEntity<TKey> : IEntity<TKey>
{
    bool IsDeleted { get; set; }
}