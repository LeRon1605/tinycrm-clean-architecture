﻿using Lab2.API.Dtos.Shared;
using Lab2.API.Validations;
using Lab2.Domain.Entities;
using System.Linq.Expressions;

namespace Lab2.API.Dtos;

public class ContactFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Contact>
{
    public string Name { get; set; } = string.Empty;

    [SortConstraint(Fields = $"{nameof(Contact.Name)}, {nameof(Contact.Email)}")]
    public override string Sorting { get; set; } = string.Empty;

    public Expression<Func<Contact, bool>> ToExpression()
    {
        return x => x.Name.Contains(Name);
    }
}