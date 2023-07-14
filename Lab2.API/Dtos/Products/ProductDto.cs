namespace Lab2.API.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Price { get; set; }
    public bool IsAvailable { get; set; }
}
