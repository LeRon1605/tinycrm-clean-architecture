using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Validations;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Products;

namespace TinyCRM.Application.Dtos.Products;

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