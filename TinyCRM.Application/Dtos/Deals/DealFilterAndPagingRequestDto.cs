using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Validations;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Deals;

namespace TinyCRM.Application.Dtos.Deals;

public class DealFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Deal>
{
    public string Title { get; set; } = string.Empty;
    public DealStatus? Status { get; set; }

    [SortConstraint(Fields = $"{nameof(Deal.Title)}")]
    public override string Sorting { get; set; } = string.Empty;

    public IPagingAndSortingSpecification<Deal, int> ToSpecification()
    {
        return new DealFilterSpecification(Page, Size, Title, Status, Sorting);
    }
}