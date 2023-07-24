namespace Lab2.API.Exceptions;

public class EntityNotFoundException : NotFoundException
{
    public EntityNotFoundException(string entityName, int id) : base($"{entityName} with id '{id}' does not exist!", ErrorCodes.ENTITY_NOT_FOUND)
    {
    }
}