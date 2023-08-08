using TinyCRM.Application.Validations;

namespace TinyCRM.Application.Dtos.Roles;

public class RoleFilterAndPagingRequestDto : PagingRequestDto
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(RoleDto.Name)}")]
    public override string Sorting { get; set; } = string.Empty;
}