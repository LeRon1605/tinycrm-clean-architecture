namespace TinyCRM.Application.Dtos.Products;

public class ProductDto : BasicProductDto
{
    public string Type { get; set; }
    public int Price { get; set; }
    public bool IsAvailable { get; set; }
}