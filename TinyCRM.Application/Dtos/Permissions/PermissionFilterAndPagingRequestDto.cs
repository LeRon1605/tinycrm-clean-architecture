using TinyCRM.Application.Validations;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Permissions;

namespace TinyCRM.Application.Dtos.Permissions;

public class PermissionFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<PermissionContent, int>
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(PermissionContent.Name)}")]
    public override string Sorting { get; set; } = string.Empty;

    public IPagingAndSortingSpecification<PermissionContent, int> ToSpecification()
    {
        return new PermissionFilterSpecification(Page, Size, Sorting, Name);
    }
}