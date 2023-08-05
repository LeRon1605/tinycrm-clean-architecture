using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Entities.Base;

namespace TinyCRM.Domain.Entities;

public class Product : Entity<int>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public ProductType Type { get; set; }
    public int Price { get; set; }
    public bool IsAvailable { get; set; }

    public ICollection<DealLine> Lines { get; set; }
}