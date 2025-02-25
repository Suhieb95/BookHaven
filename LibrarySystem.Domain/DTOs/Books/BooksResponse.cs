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
    public decimal DiscountPercentage { get; init; }
    public decimal DiscountedPrice { get; private set; }
    public bool IsInStock { get; init; }
    public string[]? ImageUrls { get; set; }
    public float? Rating
    {
        get => _rating;
        set
        {
            if (value is not null and (> 5 or < 1)) // Check for Invalid Rating
                throw new InvalidBookRatingException();

            _rating = value;
        }
    }
    public void CalculateDiscountedPrice()
           => DiscountedPrice = (1 - (DiscountPercentage / 100)) * Price;
}
