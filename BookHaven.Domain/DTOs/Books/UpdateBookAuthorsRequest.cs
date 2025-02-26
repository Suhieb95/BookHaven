namespace BookHaven.Domain.DTOs.Books;
public record UpdateBookAuthorsRequest(int BookId, List<int> Authors);