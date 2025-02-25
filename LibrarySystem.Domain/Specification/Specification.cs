using System.Data;

namespace LibrarySystem.Domain.Specification;
public abstract class Specification
{
    public abstract string ToSql();
    public virtual object? Parameters { get; protected init; } = null;
    public CommandType CommandType { get; protected set; } = CommandType.Text;
}