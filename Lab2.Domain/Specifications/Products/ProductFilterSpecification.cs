using Lab2.Domain.Entities;
using System.Linq.Expressions;
using Lab2.Domain.Enums;

namespace Lab2.Domain.Specifications;

public class ProductFilterSpecification : PagingAndSortingSpecification<Product, int>, IPagingAndSortingSpecification<Product, int>
{
    private readonly string _name;
    private readonly ProductType? _type;

    public ProductFilterSpecification(int page, int size, string name, ProductType? type, string sorting) : base(page, size, sorting,false)
    {
        _name = name;
        _type = type;
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        return x => x.Name.Contains(_name) && (_type == null || x.Type == _type);
    }
}