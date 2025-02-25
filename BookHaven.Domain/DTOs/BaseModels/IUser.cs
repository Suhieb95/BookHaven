namespace BookHaven.Domain.DTOs.BaseModels;
public interface IUser
{
    IReadOnlyList<string> Permissions { get; }
    string[] Roles { get; }
}
