namespace TinyCRM.Application.Dtos.Shared;

public class PagingRequestDto
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public virtual string Sorting { get; set; } = string.Empty;
}