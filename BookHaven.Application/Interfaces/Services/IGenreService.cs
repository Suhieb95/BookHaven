using BookHaven.Domain.DTOs;
using BookHaven.Domain.Specification;
namespace BookHaven.Application.Interfaces.Services;

public interface IGenreService : IGenericWriteRepository<Genre, Genre, int>, IGenericReadWithParamRepository<List<Genre>, Specification>,
IGenericReadPaginatedRepository<PaginatedResponse<Genre>, PaginationParam>;