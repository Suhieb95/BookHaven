namespace BookHaven.Domain.BaseModels.User;
public abstract class PersonBase : BaseEntity<Guid>
{
    public PersonBase() { }
    public PersonBase(string emailAddress, string userName, Guid id, string? imageurl)
    {
        EmailAddress = emailAddress;
        UserName = userName;
        Id = id;
        ImageUrl = imageurl;
    }
    public string EmailAddress { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string? ImageUrl { get; set; }
};