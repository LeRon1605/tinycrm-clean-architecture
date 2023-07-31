﻿using AutoMapper;
using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Mapper;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<Account, BasicAccountDto>();

        CreateMap<Account, AccountDto>();
        CreateMap<PagedResultDto<Account>, PagedResultDto<AccountDto>>();
        CreateMap<AccountCreateDto, Account>();
        CreateMap<AccountUpdateDto, Account>();
    }
}