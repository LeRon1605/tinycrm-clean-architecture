﻿using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;

namespace Lab2.Infrastructure.Repositories;

public class UserRepository : Repository<User, string>, IUserRepository
{
    public UserRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}