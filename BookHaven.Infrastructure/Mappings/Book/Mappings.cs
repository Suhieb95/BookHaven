using BookHaven.Domain.DTOs.Books;
using BookHaven.Infrastructure.Parameters.Book;

namespace BookHaven.Infrastructure.Mappings.Book;
internal static class Mappings
{
    internal static CreateBookParameters ToParameter(this CreateBookRequest request)
      => new()
      {
          Title = request.Title,
          ISBN = request.ISBN,
          PublishedYear = request.PublishedYear,
          Price = request.Price,
          Quantity = request.Quantity,
          DiscountPercentage = request.DiscountPercentage
      };
    internal static UpdateBookParameters ToParameter(this UpdateBookRequest request)
    => new()
    {
        Id= request.Id,
        Title = request.Title,
        ISBN = request.ISBN,
        PublishedYear = request.PublishedYear,
        Price = request.Price,
        Quantity = request.Quantity,
        DiscountPercentage = request.DiscountPercentage
    };
}