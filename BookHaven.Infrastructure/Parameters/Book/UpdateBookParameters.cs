namespace BookHaven.Infrastructure.Parameters.Book;

public class UpdateBookParameters
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public short PublishedYear { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 0;
    public decimal? DiscountPercentage { get; set; }
}