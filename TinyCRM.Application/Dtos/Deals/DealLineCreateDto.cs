using System.ComponentModel.DataAnnotations;

namespace TinyCRM.Application.Dtos.Deals;

public class DealLineCreateDto
{
    [Required]
    public int PricePerUnit { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int ProductId { get; set; }
}