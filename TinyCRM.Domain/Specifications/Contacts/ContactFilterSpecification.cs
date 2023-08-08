using System.Linq.Expressions;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Domain.Specifications.Contacts;

public class ContactFilterSpecification : PagingAndSortingSpecification<Contact, int>, IPagingAndSortingSpecification<Contact, int>
{
    private readonly string _name;

    public ContactFilterSpecification(int page, int size, string name, string sorting) : base(page, size, sorting)
    {
        _name = name;
    }

    public override Expression<Func<Contact, bool>> ToExpression()
    {
        return x => x.Name.Contains(_name);
    }
}