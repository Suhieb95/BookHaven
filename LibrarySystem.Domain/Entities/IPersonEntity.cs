namespace LibrarySystem.Domain.Entities;

public interface IPersonEntity
{
    string Password { get; }
    bool IsActive { get; }
    DateTime? LastLogin { get; }
    DateTime CreatedAt { get; }
}
