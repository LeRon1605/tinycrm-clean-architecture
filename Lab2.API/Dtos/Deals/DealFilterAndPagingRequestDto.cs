using Lab2.API.Dtos.Shared;
using Lab2.Domain.Entities;
using Lab2.Domain.Shared.Enums;
using System.Linq.Expressions;

namespace Lab2.API.Dtos;

public class DealFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Deal>
{
    public string Title { get; set; } = string.Empty;
    public DealStatus? Status { get; set; }
    

    public Expression<Func<Deal, bool>> ToExpression()
    {
        return x => x.Title.Contains(Title) && (Status == null || x.Status == Status);
    }
}
