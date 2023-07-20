using Lab2.API.Dtos.Shared;
using Lab2.Domain.Entities;
using Lab2.Domain.Shared.Enums;
using System.Linq.Expressions;

namespace Lab2.API.Dtos;

public class ProductFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Product>
{
    public string Name { get; set; } = string.Empty;
    public ProductType? Type { get; set; }

    public Expression<Func<Product, bool>> ToExpression()
    {
        return x => x.Name.Contains(Name) && (Type == null || x.Type == Type);
    }
}