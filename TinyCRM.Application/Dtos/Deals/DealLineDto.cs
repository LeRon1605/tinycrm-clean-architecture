using TinyCRM.Application.Dtos.Products;

namespace TinyCRM.Application.Dtos.Deals;

public class DealLineDto
{
    public int Id { get; set; }
    public int PricePerUnit { get; set; }
    public int Quantity { get; set; }
    public int TotalAmount { get; set; }
    public BasicProductDto Product { get; set; }
}