using Lab2.API.Dtos.Shared;
using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.API.Dtos;

public class AccountFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Account>
{
    public string Name { get; set; } = string.Empty;

    public Expression<Func<Account, bool>> ToExpression()
    {
        return x => x.Name.Contains(Name);
    }
}
