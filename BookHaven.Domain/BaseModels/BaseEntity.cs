namespace BookHaven.Domain.BaseModels;
public abstract class BaseEntity<T>
{
    public T Id { get; set; } = default!;
}