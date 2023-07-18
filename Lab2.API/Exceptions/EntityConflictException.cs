namespace Lab2.API.Exceptions;

public class EntityConflictException : ConflictException
{
    public EntityConflictException(string entityName, string column, string value) : base($"{entityName} with {column} '{value}' has aleady existed!")
    {
    }
}
