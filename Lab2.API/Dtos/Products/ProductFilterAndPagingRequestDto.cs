namespace Lab2.API.Dtos;

public class ProductFilterAndPagingRequestDto
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string Title { get; set; } = string.Empty;
}
