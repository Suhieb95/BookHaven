using BookHaven.Domain.BaseModels;

namespace BookHaven.Domain.Entities;

public class Author : BaseEntity<int>
{
    public required string Name { get; set; }
    public required DateTime DateOfBirth { get; set; }
}