using TinyCRM.Application.Validations;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Leads;

namespace TinyCRM.Application.Dtos.Leads;

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