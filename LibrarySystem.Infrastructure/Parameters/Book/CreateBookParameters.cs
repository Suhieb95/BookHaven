namespace LibrarySystem.Infrastructure.Parameters.Book;
public class CreateBookParameters
{
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public short PublishedYear { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 0;
    public decimal? DiscountPercentage { get; set; }
}
