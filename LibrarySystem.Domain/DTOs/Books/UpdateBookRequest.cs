using LibrarySystem.Domain.BaseModels;
using Microsoft.AspNetCore.Http;
namespace LibrarySystem.Domain.DTOs.Books;
public class UpdateBookRequest : BaseEntity<int>
{
    public required string Title { get; set; }
    public required string ISBN { get; set; }
    public short PublishedYear { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 0;
    public IFormFileCollection? Images { get; set; }
}
