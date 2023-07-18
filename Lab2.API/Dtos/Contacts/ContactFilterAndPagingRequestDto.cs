﻿using System.Linq.Expressions;
using Lab2.API.Dtos.Shared;
using Lab2.Domain.Entities;

namespace Lab2.API.Dtos;

public class ContactFilterAndPagingRequestDto : PagingRequestDto, IFilterDto<Contact>
{
    public string Name { get; set; } = string.Empty;

    public Expression<Func<Contact, bool>> ToExpression()
    {
        return x => x.Name.Contains(Name);
    }
}
