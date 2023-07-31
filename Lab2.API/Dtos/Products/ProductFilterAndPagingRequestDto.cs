using Lab2.API.Dtos.Shared;
using Lab2.API.Validations;
using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using System.Linq.Expressions;
using Lab2.Domain.Specifications;

namespace Lab2.API.Dtos;

public class ProductFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Product>
{
    public string Name { get; set; } = string.Empty;
    public ProductType? Type { get; set; }

    [SortConstraint(Fields = $"{nameof(Product.Code)}, {nameof(Product.Name)}, {nameof(Product.Price)}")]
    public override string Sorting { get; set; } = string.Empty;

    public IPagingAndSortingSpecification<Product, int> ToSpecification()
    {
        return new ProductFilterSpecification(Page, Size, Name, Type, Sorting);
    }
}