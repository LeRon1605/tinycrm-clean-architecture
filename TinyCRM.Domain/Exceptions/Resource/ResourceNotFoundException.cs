namespace TinyCRM.Domain.Exceptions.Resource;

public class ResourceNotFoundException : CoreException
{
    public ResourceNotFoundException(string message) : base(message)
    {
    }

    public ResourceNotFoundException(string entityName, int id) : base($"{entityName} with id '{id}' does not exist!", ErrorCodes.ResourceNotFound)
    {
    }

    public ResourceNotFoundException(string entityName, string id) : base($"{entityName} with id '{id}' does not exist!", ErrorCodes.ResourceNotFound)
    {
    }

    public ResourceNotFoundException(string entityName, string column, string value) : base($"{entityName} with {column} '{value}' does not exist!", ErrorCodes.ResourceNotFound)
    {
    }
}