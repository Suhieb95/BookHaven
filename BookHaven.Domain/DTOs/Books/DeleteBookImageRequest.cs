namespace BookHaven.Domain.DTOs.Books;
public record class DeleteBookImageRequest(int BookId, string[] Paths);