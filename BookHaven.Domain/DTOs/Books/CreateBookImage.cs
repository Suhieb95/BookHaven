namespace BookHaven.Domain.DTOs.Books;

public class CreateBookImage
{
    public CreateBookImage() { }
    public CreateBookImage(int bookId, string imageUrl)
    {
        BookId = bookId;
        ImageUrl = imageUrl;
    }
    public int BookId { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}
