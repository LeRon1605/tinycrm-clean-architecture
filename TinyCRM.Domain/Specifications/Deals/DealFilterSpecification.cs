using System.Linq.Expressions;
using TinyCRM.Domain.Common.Enums;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Specifications.Deals;

public class DealFilterSpecification : PagingAndSortingSpecification<Deal, int>, IPagingAndSortingSpecification<Deal, int>
{
    private readonly string _title;
    private readonly DealStatus? _status;

    public DealFilterSpecification(int page, int size, string title, DealStatus? status, string sorting) : base(page, size, sorting)
    {
        _title = title;
        _status = status;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        return x => x.Title.Contains(_title) && (_status == null || x.Status == _status);
    }
}