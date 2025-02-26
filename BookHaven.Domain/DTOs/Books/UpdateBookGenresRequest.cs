namespace BookHaven.Domain.DTOs.Books;
public record UpdateBookGenresRequest(int BookId, List<int> Genres);