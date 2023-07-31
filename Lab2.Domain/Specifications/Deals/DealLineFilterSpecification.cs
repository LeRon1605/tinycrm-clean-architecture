using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public class DealLineFilterSpecification : PagingAndSortingSpecification<DealLine, int>, IPagingAndSortingSpecification<DealLine, int>
{
    private readonly string _name;

    public DealLineFilterSpecification(int page, int size, string name, string sorting) : base(page, size, sorting, false)
    {
        _name = name;

        AddInclude(x => x.Product);
    }

    public override Expression<Func<DealLine, bool>> ToExpression()
    {
        return x => x.Product.Name.Contains(_name);
    }

    public override string BuildSorting()
    {
        Sorting = Sorting.Replace("Code", "Product.Code");
        Sorting = Sorting.Replace("Name", "Product.Name");
        Sorting = Sorting.Replace("TotalAmount", "Quantity * PricePerUnit");
        return Sorting;
    }
}