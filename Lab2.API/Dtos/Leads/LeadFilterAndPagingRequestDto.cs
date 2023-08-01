using Lab2.API.Dtos.Shared;
using Lab2.API.Validations;
using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using Lab2.Domain.Specifications;

namespace Lab2.API.Dtos;

public class LeadFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Lead>
{
    public string Title { get; set; } = string.Empty;
    public LeadStatus? Status { get; set; }

    [SortConstraint(Fields = $"{nameof(Lead.Title)}, {nameof(Lead.Customer)}, {nameof(Lead.EstimatedRevenue)}")]
    public override string Sorting { get; set; } = string.Empty;

    public IPagingAndSortingSpecification<Lead, int> ToSpecification()
    {
        return new LeadFilterSpecification(Page, Size, Title, Status, Sorting);
    }
}