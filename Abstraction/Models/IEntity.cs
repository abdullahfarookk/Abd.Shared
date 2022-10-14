namespace Abd.Shared.Abstraction.Models;

public interface IEntity:IModel
{
    public Guid Id { get; set; }
}
public interface IEntity<T>:IEntity
{
    public new T Id { get; set; }
}