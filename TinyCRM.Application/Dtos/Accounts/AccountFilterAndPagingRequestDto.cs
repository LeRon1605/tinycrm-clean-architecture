using TinyCRM.Application.Validations;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Accounts;

namespace TinyCRM.Application.Dtos.Accounts;

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