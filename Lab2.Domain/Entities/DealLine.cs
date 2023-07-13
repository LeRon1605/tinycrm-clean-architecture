namespace Lab2.Domain.Entities;

public class DealLine : IEntity<int>
{
    public int Id { get; set; }
    public int PricePerUnit { get; set; }
    public int Quantity { get; set; }
    
    public string ProductId { get; set; }
    public Product Product { get; set; }

    public int DealId { get; set; }
    public Deal Deal { get; set; }
}
