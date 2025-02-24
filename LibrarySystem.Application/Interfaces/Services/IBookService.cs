using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Domain.Specification;

namespace LibrarySystem.Application.Interfaces.Services;
public interface IBookService : IGenericReadWithParamRepository<List<BookResponse>, Specification>, IGenericReadPaginatedRepository<PaginatedResponse<BookResponse>, PaginationParam>;
