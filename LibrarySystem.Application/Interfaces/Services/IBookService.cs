using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;

namespace LibrarySystem.Application.Interfaces.Services;
public interface IBookService : IGenericReadWithParamRepository<PaginatedResponse<BooksResponse>, PaginationParam>, IGenericReadByIdRepository<Book?, int>;
