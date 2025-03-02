using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs.Books;

namespace BookHaven.Infrastructure.Services.BookImages;
public class BookImagesService(ISqlDataAccess sqlDataAccess) : IBookImagesService
{
    private readonly ISqlDataAccess _sqlDataAccess = sqlDataAccess;
    public async Task Add(CreateBookImage createBookImage, CancellationToken? cancellationToken = null)
    {
        const string Sql = "INSERT INTO BookImages (ImageUrl, BookId) VALUES (@ImageUrl, @BookId)";
        await _sqlDataAccess.SaveData<int>(Sql, createBookImage, cancellationToken: cancellationToken);
    }
    public async Task Delete(UpdateBookImagesResult request, CancellationToken? cancellationToken = null)
    {
        IEnumerable<Task>? tasks = request.Paths.Select(p => DeleteBookImage(request.BookId, p, cancellationToken));
        await Task.WhenAll(tasks);
    }
    public async Task Update(UpdateBookImagesResult request, CancellationToken? cancellationToken = null)
    {
        IEnumerable<Task>? tasks = request.Paths.Select(p => UpdateBookImage(request.BookId, p, cancellationToken));
        await Task.WhenAll(tasks);
    }
    private async Task DeleteBookImage(int id, string path, CancellationToken? cancellationToken = default)
    {
        const string Sql = "DELETE FROM BookImages WHERE BookId = @BookId AND ImageUrl = @ImageUrl";
        await _sqlDataAccess.SaveData(Sql, new { BookId = id, ImageUrl = path }, cancellationToken: cancellationToken);
    }
    private async Task UpdateBookImage(int id, string path, CancellationToken? cancellationToken = default)
    {
        const string Sql = "INSERT INTO BookImages (BookId, ImageUrl) VALUES (@BookId, @ImageUrl)";
        await _sqlDataAccess.SaveData(Sql, new { BookId = id, ImageUrl = path }, cancellationToken: cancellationToken);
    }
}