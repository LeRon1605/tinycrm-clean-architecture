namespace Lab2.API.Exceptions;

public class EntityNotFoundException : NotFoundException
{
    public EntityNotFoundException(string entityName, int id) : base($"{entityName} with id '{id}' does not exist!", ErrorCodes.EntityNotFound)
    {
    }

    public EntityNotFoundException(string entityName, string id) : base($"{entityName} with id '{id}' does not exist!", ErrorCodes.EntityNotFound)
    {
    }
}