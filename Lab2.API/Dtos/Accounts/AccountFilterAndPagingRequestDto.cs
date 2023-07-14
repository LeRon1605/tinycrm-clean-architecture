using Lab2.API.Dtos.Shared;

namespace Lab2.API.Dtos;

public class AccountFilterAndPagingRequestDto : PagingRequestDto
{
    public string Name { get; set; } = string.Empty;
}
