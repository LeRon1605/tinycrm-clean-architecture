using Lab2.API.Dtos.Shared;
using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.API.Dtos;

public class DealLineFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<DealLine>
{
    public string Name { get; set; } = string.Empty;

    public Expression<Func<DealLine, bool>> ToExpression()
    {
        return x => x.Product.Name.Contains(Name);
    }

    public override string BuildSortingParam()
    {
        Sorting = Sorting.Replace("Code", "Product.Code");
        Sorting = Sorting.Replace("Name", "Product.Name");
        Sorting = Sorting.Replace("TotalAmount", "Quantity * PricePerUnit");
        return Sorting;
    }
}