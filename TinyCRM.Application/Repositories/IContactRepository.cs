using TinyCRM.Application.Repositories.Base;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Repositories;

public interface IContactRepository : IRepository<Contact, int>
{
}