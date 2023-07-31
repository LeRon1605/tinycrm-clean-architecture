using Lab2.API.Dtos.Shared;
using Lab2.API.Validations;
using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using Lab2.Domain.Specifications;

namespace Lab2.API.Dtos;

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