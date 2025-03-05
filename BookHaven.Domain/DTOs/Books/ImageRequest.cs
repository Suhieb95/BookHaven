using Microsoft.AspNetCore.Http;
namespace BookHaven.Domain.DTOs.Books;

public class ImageRequest
{
    public IFormFile Image { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public bool IsMainImage { get; set; }
}