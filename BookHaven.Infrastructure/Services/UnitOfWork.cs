using BookHaven.Application.Interfaces;
using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Repositories;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Infrastructure.Services.Authors;
using BookHaven.Infrastructure.Services.Books;
using BookHaven.Infrastructure.Services.Customers;
using BookHaven.Infrastructure.Services.Genres;
using BookHaven.Infrastructure.Services.Users;

namespace BookHaven.Infrastructure.Services;
internal class UnitOfWork(ISqlDataAccess _sqlDataAccess, IDateTimeProvider _dateTimeProvider, IRedisCacheService _redisCacheService, IMssqlDbTransaction _mssqlDbTransaction)
    : IUnitOfWork
{
    // Data Access Services
    public ICustomerService Customers => new CustomerService(_sqlDataAccess, _dateTimeProvider);
    public IUserService Users => new UserService(_sqlDataAccess, _dateTimeProvider, _redisCacheService);
    public IGenreService Genres => new GenreService(_sqlDataAccess, _mssqlDbTransaction);
    public IAuthorService Authors => new AuthorService(_sqlDataAccess, _mssqlDbTransaction);
    public IBookService Books => new BookService(_sqlDataAccess, Authors, Genres, _mssqlDbTransaction);
}