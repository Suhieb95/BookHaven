using LibrarySystem.Domain.BaseModels.User;

namespace LibrarySystem.Domain.Entities;
public class User(string emailAddress, string userName, string imagePath) : PersonBase(emailAddress, userName, imagePath)
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastLogin { get; set; }
    public DateTime CreatedAt { get; set; }
};
