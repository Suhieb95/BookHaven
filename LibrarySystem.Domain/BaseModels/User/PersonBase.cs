namespace LibrarySystem.Domain.BaseModels.User;
public abstract class PersonBase : BaseEntity<Guid>
{
    public PersonBase() { }
    public PersonBase(string emailAddress, string userName, Guid id)
    {
        EmailAddress = emailAddress;
        UserName = userName;
        Id = id;
    }
    public string EmailAddress { get; set; } = default!;
    public string UserName { get; set; } = default!;
};