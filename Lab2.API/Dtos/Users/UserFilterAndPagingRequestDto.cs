using Lab2.API.Dtos.Shared;
using Lab2.API.Validations;
using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.API.Dtos;

public class UserFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<User, string>
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(User.FullName)}, {nameof(User.Email)}")]
    public override string Sorting { get; set; } = string.Empty;

    public Expression<Func<User, bool>> ToExpression()
    {
        return x => x.FullName.Contains(Name);
    }
}