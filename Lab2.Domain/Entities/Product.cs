using Lab2.Domain.Shared.Enums;

namespace Lab2.Domain.Entities;

public class Product : IEntity<string>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ProductType Type { get; set; }
    public int Price { get; set; }
    public bool IsAvailable { get; set; }

    public ICollection<DealLine> Lines { get; set; }  
}