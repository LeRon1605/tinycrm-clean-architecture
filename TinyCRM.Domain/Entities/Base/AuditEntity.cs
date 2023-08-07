namespace TinyCRM.Domain.Entities.Base;

public class AuditEntity<TKey> : SoftDeleteEntity<TKey>, IAuditEntity<TKey>
{
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
}