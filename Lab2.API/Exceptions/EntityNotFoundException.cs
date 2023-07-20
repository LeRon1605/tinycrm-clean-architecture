namespace Lab2.API.Exceptions;

public class EntityNotFoundException : NotFoundException
{
    public EntityNotFoundException(string entityName) : base($"{entityName} does not exist!")
    {
    }

    public EntityNotFoundException(string entityName, int id) : base($"{entityName} with id '{id}' does not exist!")
    {
    }

    public EntityNotFoundException(string entityName, string column, int id) : base($"{entityName} with {column} '{id}' does not exist!")
    {
    }
}