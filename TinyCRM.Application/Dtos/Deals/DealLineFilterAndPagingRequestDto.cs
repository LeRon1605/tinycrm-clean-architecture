using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Validations;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Deals;

namespace TinyCRM.Application.Dtos.Deals;

public class DealLineFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<DealLine>
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(Product.Code)}, {nameof(Product.Name)}, TotalAmount")]
    public override string Sorting { get; set; } = string.Empty;

    public IPagingAndSortingSpecification<DealLine, int> ToSpecification()
    {
        return new DealLineFilterSpecification(Page, Size, Name, Sorting);
    }
}