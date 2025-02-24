using LibrarySystem.Domain.BaseModels;
namespace LibrarySystem.Domain.Entities;
public class Genre : BaseEntity<int>
{
    public string Name { get; set; } = default!;

}

