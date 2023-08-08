using System.Linq.Expressions;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Specifications.Products;

public class ProductFilterSpecification : PagingAndSortingSpecification<Product, int>, IPagingAndSortingSpecification<Product, int>
{
    private readonly string _name;
    private readonly ProductType? _type;

    public ProductFilterSpecification(int page, int size, string name, ProductType? type, string sorting) : base(page, size, sorting)
    {
        _name = name;
        _type = type;
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        return x => x.Name.Contains(_name) && (_type == null || x.Type == _type);
    }
}