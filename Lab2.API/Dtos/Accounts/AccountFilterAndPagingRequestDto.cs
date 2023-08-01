using Lab2.API.Dtos.Shared;
using Lab2.API.Validations;
using Lab2.Domain.Entities;
using Lab2.Domain.Specifications;

namespace Lab2.API.Dtos;

public class AccountFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Account>
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(Account.Name)} , {nameof(Account.Email)}")]
    public override string Sorting { get; set; } = string.Empty;

    public IPagingAndSortingSpecification<Account, int> ToSpecification()
    {
        return new AccountFilterSpecification(Page, Size, Name, Sorting);
    }
}