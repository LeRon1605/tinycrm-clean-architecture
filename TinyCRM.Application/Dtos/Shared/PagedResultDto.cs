namespace TinyCRM.Application.Dtos.Shared;

public class PagedResultDto<T>
{
    public int TotalPages { get; set; }
    public IEnumerable<T> Data { get; set; }
}