namespace BookHaven.Domain.DTOs.Books;
public class UpdateBookImagesResult(int bookId, string[] paths)
{
    public int BookId { get; init; } = bookId;
    public string[] Paths { get; init; } = paths;
}