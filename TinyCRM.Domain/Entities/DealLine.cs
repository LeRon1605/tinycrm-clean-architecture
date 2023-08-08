namespace TinyCRM.Domain.Entities;

public class DealLine : Entity<int>
{
    public int PricePerUnit { get; set; }
    public int Quantity { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int DealId { get; set; }
    public Deal Deal { get; set; }
}