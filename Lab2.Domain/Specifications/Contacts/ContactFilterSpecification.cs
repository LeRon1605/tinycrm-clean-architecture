using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.Domain.Specifications;

public class ContactFilterSpecification : PagingAndSortingSpecification<Contact, int>, IPagingAndSortingSpecification<Contact, int>
{
    private readonly string _name;

    public ContactFilterSpecification(int page, int size, string name, string sorting) : base(page, size, sorting, false)
    {
        _name = name;
    }

    public override Expression<Func<Contact, bool>> ToExpression()
    {
        return x => x.Name.Contains(_name);
    }
}