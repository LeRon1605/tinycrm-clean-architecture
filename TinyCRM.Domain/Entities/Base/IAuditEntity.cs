namespace TinyCRM.Domain.Entities.Base;

public interface IAuditEntity<TKey> : ISoftDeleteEntity<TKey>
{
    DateTime CreatedDate { get; set; }
    string CreatedBy { get; set; }
    DateTime? UpdatedDate { get; set; }
    string UpdatedBy { get; set; }
}