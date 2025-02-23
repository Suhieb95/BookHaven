using LibrarySystem.Domain.Exceptions.BooksExceptions;

namespace LibrarySystem.Domain.DTOs.Books;
public class BooksResponse
{
    private float? _rating;
    public int Id { get; init; }
    public string Title { get; init; } = default!;
    public string ISBN { get; init; } = default!;
    public string Author { get; init; } = default!;
    public string Genre { get; init; } = default!;
    public int PublishedYear { get; init; }
    public decimal Price { get; init; }
    public string[]? ImageUrl { get; set; }
    public bool IsInStock { get; init; }
    public float? Rating
    {
        get => _rating;
        set
        {
            if (value is not null and (< 1 or > 5))
                throw new InvalidBookRatingException();

            _rating = value;
        }
    }
}
