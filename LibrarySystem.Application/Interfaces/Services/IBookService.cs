using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Interfaces.Services;
public interface IBookService : IGenericReadWithParamRepository<PaginatedResponse<Book>>, IGenericReadByIdRepository<Book?, int>;
