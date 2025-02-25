namespace LibrarySystem.Domain.DTOs.Books;
public record class DeleteBookImageRequest(int Id, string[] Paths);