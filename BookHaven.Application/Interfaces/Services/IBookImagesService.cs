using BookHaven.Domain.DTOs.Books;
namespace BookHaven.Application.Interfaces.Services;
public interface IBookImagesService
{
    Task Add(CreateBookImage createBookImage, CancellationToken? cancellationToken = null);
    Task Delete(UpdateBookImagesResult request, CancellationToken? cancellationToken = null);
    Task Update(UpdateBookImagesResult request, CancellationToken? cancellationToken = null);
}