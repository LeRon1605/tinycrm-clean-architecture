using System.ComponentModel.DataAnnotations;
using TinyCRM.Domain.Common.Enums;

namespace TinyCRM.Application.Dtos.Products;

public class ProductCreateDto
{
    [Required]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public ProductType Type { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public bool IsAvailable { get; set; }
}