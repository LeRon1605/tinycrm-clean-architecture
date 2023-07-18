using System.Linq.Expressions;
using Lab2.API.Dtos.Shared;
using Lab2.Domain.Entities;
using Lab2.Domain.Shared.Enums;

namespace Lab2.API.Dtos;

public class LeadFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Lead>
{
    public string Title { get; set; } = string.Empty;
    public LeadStatus? Status { get; set; }

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
