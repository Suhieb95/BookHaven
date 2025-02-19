namespace LibrarySystem.Domain.BaseModels.User;
public interface IUser
{
    IReadOnlyList<string> Permissions { get; }
}
