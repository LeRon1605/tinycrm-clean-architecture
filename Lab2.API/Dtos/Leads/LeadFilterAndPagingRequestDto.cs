using Lab2.API.Dtos.Shared;
using Lab2.API.Validations;
using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using System.Linq.Expressions;

namespace Lab2.API.Dtos;

public class LeadFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Lead>
{
    public string Title { get; set; } = string.Empty;
    public LeadStatus? Status { get; set; }

    [SortConstraint(Fields = $"{nameof(Lead.Title)}, {nameof(Lead.Customer)}, {nameof(Lead.EstimatedRevenue)}")]
    public override string Sorting { get; set; } = string.Empty;

    public Expression<Func<Lead, bool>> ToExpression()
    {
        return x => x.Title.Contains(Title) && (Status == null || x.Status == Status);
    }

    public override string BuildSortingParam()
    {
        Sorting = Sorting.Replace("Customer", "Cusomter.Name");
        return Sorting;
    }
}