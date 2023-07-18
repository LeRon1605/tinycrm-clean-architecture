using Lab2.Domain.Entities;
using Lab2.Domain.Repositories;
using Lab2.Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Lab2.Infrastructure.Repositories;

public class DealLineRepository : Repository<DealLine>, IDealLineRepository
{
    public DealLineRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}