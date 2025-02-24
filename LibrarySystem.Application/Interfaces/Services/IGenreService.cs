using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.Specification;
namespace LibrarySystem.Application.Interfaces.Services;

public interface IGenreService : IGenericWriteRepository<Genre, Genre, int>, IGenericReadWithParamRepository<List<Genre>, Specification>,
IGenericReadPaginatedRepository<PaginatedResponse<Genre>, PaginationParam>;