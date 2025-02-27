using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Repositories;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Infrastructure.Services.Authors;
using BookHaven.Infrastructure.Services.BookImages;
using BookHaven.Infrastructure.Services.Books;
using BookHaven.Infrastructure.Services.Customers;
using BookHaven.Infrastructure.Services.Genres;
using BookHaven.Infrastructure.Services.Users;

namespace BookHaven.Infrastructure.Services;
internal class UnitOfWork(ISqlDataAccess sqlDataAccess, IDateTimeProvider dateTimeProvider, IRedisCacheService redisCacheService, IMssqlDbTransaction mssqlDbTransaction)
    : IUnitOfWork
{
    // Data Access Services
    private ICustomerService? _customers;
    private IUserService? _users;
    private IGenreService? _genres;
    private IAuthorService? _authors;
    private IBookImagesService? _bookImages;
    private IBookService? _books;

    // Lazy Initialization for Services
    public ICustomerService Customers => _customers ??= new CustomerService(sqlDataAccess, dateTimeProvider);
    public IUserService Users => _users ??= new UserService(sqlDataAccess, dateTimeProvider, redisCacheService);
    public IGenreService Genres => _genres ??= new GenreService(sqlDataAccess, mssqlDbTransaction);
    public IAuthorService Authors => _authors ??= new AuthorService(sqlDataAccess, mssqlDbTransaction);
    public IBookImagesService BookImages => _bookImages ??= new BookImagesService(sqlDataAccess);
    public IBookService Books => _books ??= new BookService(sqlDataAccess, this, mssqlDbTransaction);
}