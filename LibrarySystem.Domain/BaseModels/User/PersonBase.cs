namespace LibrarySystem.Domain.BaseModels.User;
public abstract class PersonBase(string emailAddress, string userName, string? imageurl)
{
    public string EmailAddress { get; } = emailAddress;
    public string UserName { get; } = userName;
    public string? ImageUrl { get; } = imageurl;
};