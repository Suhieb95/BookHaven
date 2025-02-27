using BookHaven.Domain.DTOs;
using BookHaven.Domain.DTOs.Books;
using BookHaven.Domain.Specification;

namespace BookHaven.Application.Interfaces.Services;
public interface IBookService : IGenericReadPaginatedRepository<PaginatedResponse<BookResponse>, PaginationParam>,
IGenericReadByIdRepository<BookResponse, Specification>,
IGenericWriteRepository<CreateBookRequest, UpdateBookRequest, int>,
IGenericReadRepository;