using LibrarySystem.Domain.BaseModels.User;

namespace LibrarySystem.Domain.Entities;
public class User : PersonBase, IPersonEntity
{
    public string Password { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? ImageUrl { get; set; }
}
