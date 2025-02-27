namespace BookHaven.Domain.DTOs.Books;
public class UpdateBookImagesRequest
{
    public UpdateBookImagesRequest(int bookId, string[] paths)
    {
        BookId = bookId;
        Paths = paths;
    }
    public int BookId { get; init; }
    public string[] Paths { get; init; }
}