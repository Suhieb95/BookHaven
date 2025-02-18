using LibrarySystem.Domain.DTOs.BaseModels;

namespace LibrarySystem.Domain.DTOs.Users;
public class User(string emailAddress, string userName, string imagePath) : PersonBase(emailAddress, userName, imagePath)
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastLogin { get; set; }
    public DateTime CreatedAt { get; set; }
};
