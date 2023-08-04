using Lab2.Domain.Entities;
using Lab2.Domain.Enums;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public class LeadFilterSpecification : PagingAndSortingSpecification<Lead, int>, IPagingAndSortingSpecification<Lead, int>
{
    private readonly string _title;
    private readonly LeadStatus? _status;

    public LeadFilterSpecification(int page, int size, string title, LeadStatus? status, string sorting) : base(page, size, sorting)
    {
        _title = title;
        _status = status;

        AddInclude(x => x.Customer);
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return x => x.Title.Contains(_title) && (_status == null || x.Status == _status);
    }

    public override string BuildSorting()
    {
        Sorting = Sorting.Replace("Customer", "Customer.Name");
        return Sorting;
    }
}