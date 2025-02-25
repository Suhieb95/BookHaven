using BookHaven.Domain.BaseModels;
namespace BookHaven.Domain.Entities;
public class Genre : BaseEntity<int>
{
    public string Name { get; set; } = default!;

}

