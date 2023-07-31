using Lab2.API.Dtos.Shared;
using Lab2.API.Validations;
using Lab2.Domain.Entities;
using Lab2.Domain.Specifications;

namespace Lab2.API.Dtos;

public class DealLineFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<DealLine>
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(Product.Code)}, {nameof(Product.Name)}, TotalAmount")]
    public override string Sorting { get; set; } = string.Empty;

    public IPagingAndSortingSpecification<DealLine, int> ToSpecification()
    {
        return new DealLineFilterSpecification(Page, Size, Name, Sorting);
    }
}