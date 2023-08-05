using TinyCRM.Application.Dtos.Shared;
using TinyCRM.Application.Validations;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Specifications.Abstracts;
using TinyCRM.Domain.Specifications.Contacts;

namespace TinyCRM.Application.Dtos.Contacts;

public class ContactFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Contact>
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(Contact.Name)}, {nameof(Contact.Email)}")]
    public override string Sorting { get; set; } = string.Empty;

    public IPagingAndSortingSpecification<Contact, int> ToSpecification()
    {
        return new ContactFilterSpecification(Page, Size, Name, Sorting);
    }
}