using Lab2.Domain.Entities;
using Lab2.Domain.Repositories.Interfaces;

namespace Lab2.Domain.Repositories;

public interface IContactRepository : IRepository<Contact, int>
{
}