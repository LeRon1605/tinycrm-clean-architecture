namespace Lab2.API.Dtos;

public class DealLineCreateDto
{
    public int PricePerUnit { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int DealId { get; set; }
}