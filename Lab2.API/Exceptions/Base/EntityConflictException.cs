namespace Lab2.API.Exceptions;

public class EntityConflictException : ConflictException
{
    public EntityConflictException(string entityName, string column, string value) : base($"{entityName} with {column} '{value}' has already existed!", ErrorCodes.ENTITY_CONFLICT)
    {
    }

    public EntityConflictException(string entityName, string column, string value, string errorCode) : base($"{entityName} with {column} '{value}' has already existed!", errorCode)
    {
    }
}