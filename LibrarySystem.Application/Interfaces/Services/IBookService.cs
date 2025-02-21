using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.DTOs.Books;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Interfaces.Services;
public interface IBookService : IGenericReadWithParamRepository<PaginatedResponse<BookResponse>, PaginationParam>, IGenericReadByIdRepository<Book?, int>;
