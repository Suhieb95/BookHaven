using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Books;
using BookHaven.Domain.Specification;
namespace BookHaven.Application.Interfaces.Services;

public interface IGenreService : IGenericWriteRepository<Genre, Genre, int>,
IGenericReadPaginatedRepository<PaginatedResponse<Genre>, PaginationParam>
{
    Task<List<T>> GetAll<T>(Specification<T> specification, CancellationToken? cancellationToken = default);
    Task UpdateBookGenres(UpdateBookGenresRequest request, CancellationToken? cancellationToken = default);
}