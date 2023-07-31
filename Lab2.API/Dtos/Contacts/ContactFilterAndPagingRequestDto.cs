using Lab2.API.Dtos.Shared;
using Lab2.API.Validations;
using Lab2.Domain.Entities;
using Lab2.Domain.Specifications;

namespace Lab2.API.Dtos;

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