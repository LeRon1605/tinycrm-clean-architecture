using Lab2.Domain.Shared.Enums;

namespace Lab2.API.Dtos;

public class ProductCreateDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ProductType Type { get; set; }
    public int Price { get; set; }
    public bool IsAvailable { get; set; }
}
