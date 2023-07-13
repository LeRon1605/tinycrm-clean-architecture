namespace Lab2.API.Dtos;

public class DealLineDto
{
    public int Id { get; set; }
    public int PricePerUnit { get; set; }
    public int Quantity { get; set; }
    public int TotalAmount { get; set; }
    public int DealId { get; set; }
    public ProductDto Product { get; set; }
}
