using Lab2.API.Dtos.Shared;
using Lab2.Domain.Shared.Enums;

namespace Lab2.API.Dtos;

public class ProductFilterAndPagingRequestDto : PagingRequestDto
{
    public string Name { get; set; } = string.Empty;
    public ProductType? Type { get; set; }
}
