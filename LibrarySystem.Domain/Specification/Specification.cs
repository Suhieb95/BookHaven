namespace LibrarySystem.Domain.Specification;
public abstract class Specification
{
    public abstract string ToSql();
    public virtual object? Parameters { get; protected init; } = null;
}