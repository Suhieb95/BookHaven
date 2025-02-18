namespace LibrarySystem.Domain.Specification;
public abstract class Specification
{
    public abstract string ToSql();
    public dynamic? Parameters { get; protected init; }
}
