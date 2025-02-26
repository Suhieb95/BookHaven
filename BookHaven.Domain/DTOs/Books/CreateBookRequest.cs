using Microsoft.AspNetCore.Http;
namespace BookHaven.Domain.DTOs.Books;
public class CreateBookRequest
{
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public required short PublishedYear { get; set; }
    public required decimal Price { get; set; }
    public int Quantity { get; set; }
    public List<int> Authors { get; set; } = [];
    public List<int> Genres { get; set; } = [];
    public decimal? DiscountPercentage { get; set; }
    public IFormFileCollection? Images { get; set; }
}