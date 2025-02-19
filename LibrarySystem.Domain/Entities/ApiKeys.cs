namespace LibrarySystem.Domain.Entities;
public record class ApiKeys
{
    public required string UpdateLateFines { get; init; }
    public required string CheckLateBorrows { get; init; }
};
