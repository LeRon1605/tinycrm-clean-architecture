namespace TinyCRM.Domain.Entities.Base;

public interface IEntity<T>
{
    public T Id { get; set; }
}