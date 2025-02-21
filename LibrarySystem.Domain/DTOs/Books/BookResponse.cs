namespace LibrarySystem.Domain.DTOs.Books;
public class BookResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public string ISBN { get; init; } = default!;
    public string Author { get; init; } = default!;
    public string Genre { get; init; } = default!;
    public int PublishedYear { get; init; }
    public decimal Price { get; init; }
    public string[]? ImageUrl { get; set; }
    public bool IsInStock { get; init; }
}
