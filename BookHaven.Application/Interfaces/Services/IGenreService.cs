using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Books;
namespace BookHaven.Application.Interfaces.Services;

public interface IGenreService : IGenericWriteRepository<Genre, Genre, int>,
IGenericReadPaginatedRepository<PaginatedResponse<Genre>, PaginationParam>,
IGenericSpecificationReadRepository
{
    Task UpdateBookGenres(UpdateBookGenresRequest request, CancellationToken? cancellationToken = default);
}