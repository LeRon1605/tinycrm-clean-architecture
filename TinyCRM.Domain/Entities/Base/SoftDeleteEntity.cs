namespace TinyCRM.Domain.Entities.Base;

public class SoftDeleteEntity<TKey> : Entity<TKey>, ISoftDeleteEntity<TKey>
{
    public bool IsDeleted { get; set; }
}