namespace Lab2.Domain.Base;

public interface IEntity<T>
{
    public T Id { get; set; }
}