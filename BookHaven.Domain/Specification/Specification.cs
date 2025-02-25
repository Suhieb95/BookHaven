using System.Data;

namespace BookHaven.Domain.Specification;
public abstract class Specification
{
    public abstract string ToSql();
    public virtual object? Parameters { get; protected init; } = null;
    public CommandType CommandType { get; protected set; } = Text;
}
public abstract class Specification<T> : Specification;