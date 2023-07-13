namespace Lab2.API.Dtos;

public class ContactFilterAndPagingRequestDto
{
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string Name { get; set; } = string.Empty;
}
