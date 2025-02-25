using Microsoft.AspNetCore.Http;

namespace BookHaven.Domain.DTOs.Books;
public record class UpdateBookImageRequest
{
    public UpdateBookImageRequest() { }

    public required int Id { get; set; }
    public required IFormFileCollection Images { get; set; }
}