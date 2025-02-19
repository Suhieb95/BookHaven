namespace LibrarySystem.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string ISBN { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Genre { get; set; } = default!;
    public int PublishedYear { get; set; }
    public bool IsAvailable { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
}
