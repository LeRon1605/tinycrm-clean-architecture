namespace Lab2.API.Dtos;

public class PagedResultDto<T>
{
    public int TotalPages { get; set; }
    public IEnumerable<T> Data { get; set; }
}
