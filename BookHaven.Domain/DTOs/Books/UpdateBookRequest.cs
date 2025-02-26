using BookHaven.Domain.BaseModels;
namespace BookHaven.Domain.DTOs.Books;
public class UpdateBookRequest : BaseEntity<int>
{
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public required short PublishedYear { get; set; }
    public required decimal Price { get; set; }
    public List<int> Authors { get; set; } = [];
    public List<int> Genres { get; set; } = [];
    public int Quantity { get; set; } = 0;
    public decimal? DiscountPercentage { get; set; }
}