using Lab2.API.Dtos.Shared;

namespace Lab2.API.Dtos;

public class ContactFilterAndPagingRequestDto : PagingRequestDto
{
    public string Name { get; set; } = string.Empty;
}
