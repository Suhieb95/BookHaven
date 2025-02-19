namespace LibrarySystem.Domain.BaseModels.User;
public abstract class PersonBase
{
    public PersonBase() { }
    public PersonBase(string emailAddress, string userName, string? imageUrl, Guid id)
    {
        EmailAddress = emailAddress;
        UserName = userName;
        ImageUrl = imageUrl;
        Id = id;
    }
    public Guid Id { get; set; }
    public string EmailAddress { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string? ImageUrl { get; set; }
};