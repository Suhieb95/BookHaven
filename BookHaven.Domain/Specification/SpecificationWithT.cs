using System.Data;

namespace BookHaven.Domain.Specification;
public abstract class SpecificationWithT<T>
{
    public abstract string ToSql();
    public virtual object? Parameters { get; protected init; } = null;
    public CommandType CommandType { get; protected set; } = Text;
}