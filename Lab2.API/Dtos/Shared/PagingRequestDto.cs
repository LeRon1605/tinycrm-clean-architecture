namespace Lab2.API.Dtos.Shared;

public class PagingRequestDto
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
}
