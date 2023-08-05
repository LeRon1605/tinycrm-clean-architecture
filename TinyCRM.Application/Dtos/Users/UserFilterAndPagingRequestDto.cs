using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Validations;

namespace TinyCRM.Application.Dtos.Users;

public class UserFilterAndPagingRequestDto : PagingRequestDto
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(UserDto.FullName)}, ${nameof(UserDto.Email)}")]
    public override string Sorting { get; set; } = string.Empty;
}