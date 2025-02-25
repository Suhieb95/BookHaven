namespace BookHaven.Domain.DTOs.Books;
public record class DeleteBookImageRequest(int Id, string[] Paths);