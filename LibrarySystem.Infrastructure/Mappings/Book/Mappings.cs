using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Infrastructure.Parameters.Book;

namespace LibrarySystem.Infrastructure.Mappings.Book;
internal static class Mappings
{
    internal static CreateBookParameters ToParameter(this CreateBookRequest request)
      => new()
      {
          Title = request.Title,
          ISBN = request.ISBN,
          PublishedYear = request.PublishedYear,
          AuthorId = request.AuthorId,
          GenreId = request.GenreId,
          Price = request.Price,
          Quantity = request.Quantity,
          DiscountPercentage = request.DiscountPercentage
      };
}