using Microsoft.AspNetCore.Http;

namespace LibrarySystem.Domain.DTOs.Books;
public record class UpdateBookImageRequest
{
    public UpdateBookImageRequest() { }

    public required int Id { get; set; }
    public required IFormFileCollection Images { get; set; }
}