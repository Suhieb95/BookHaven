using BookHaven.Application.Interfaces.Database;
using BookHaven.Application.Interfaces.Repositories;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Infrastructure.Services.Auth;
using BookHaven.Infrastructure.Services.Authors;
using BookHaven.Infrastructure.Services.BookImages;
using BookHaven.Infrastructure.Services.Books;
using BookHaven.Infrastructure.Services.Customers;
using BookHaven.Infrastructure.Services.Genres;
using BookHaven.Infrastructure.Services.Users;

namespace BookHaven.Infrastructure.Services;
internal class UnitOfWork(ISqlDataAccess sqlDataAccess, IDateTimeProvider dateTimeProvider, IRedisCacheService redisCacheService, ICacheValidator cacheValidator, IMssqlDbTransaction mssqlDbTransaction)
    : IUnitOfWork
{
    private readonly ISqlDataAccess _sqlDataAccess = sqlDataAccess;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly IRedisCacheService _redisCacheService = redisCacheService;
    private readonly ICacheValidator _cacheValidator = cacheValidator;
    private readonly IMssqlDbTransaction _mssqlDbTransaction = mssqlDbTransaction;

    // Data Access Services
    private ICustomerService? _customers;
    private IUserService? _users;
    private IGenreService? _genres;
    private IAuthorService? _authors;
    private IBookImagesService? _bookImages;
    private IBookService? _books;
    private IUserSecurityService? _userSecurity;

    // Lazy Initialization for Services
    public ICustomerService Customers => _customers ??= new CustomerService(_sqlDataAccess, _dateTimeProvider);
    public IUserService Users => _users ??= new UserService(_sqlDataAccess, _dateTimeProvider, _redisCacheService, _cacheValidator);
    public IGenreService Genres => _genres ??= new GenreService(_sqlDataAccess, _mssqlDbTransaction);
    public IAuthorService Authors => _authors ??= new AuthorService(_sqlDataAccess, _mssqlDbTransaction);
    public IBookImagesService BookImages => _bookImages ??= new BookImagesService(_sqlDataAccess);
    public IBookService Books => _books ??= new BookService(_sqlDataAccess, Authors, Genres, _mssqlDbTransaction);
    public IUserSecurityService UserSecurity => _userSecurity ??= new UserSecurityService(_sqlDataAccess);
}