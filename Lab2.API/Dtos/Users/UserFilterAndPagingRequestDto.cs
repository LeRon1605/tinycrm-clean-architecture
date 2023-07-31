using Lab2.API.Dtos.Shared;
using Lab2.API.Validations;
using Lab2.Domain.Entities;
using Lab2.Domain.Specifications;
using Lab2.Domain.Specifications.Users;

namespace Lab2.API.Dtos;

public class UserFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<User, string>
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(User.FullName)}, {nameof(User.Email)}")]
    public override string Sorting { get; set; } = string.Empty;

    public IPagingAndSortingSpecification<User, string> ToSpecification()
    {
        return new UserFilterSpecification(Page, Size, Name, Sorting);
    }
}