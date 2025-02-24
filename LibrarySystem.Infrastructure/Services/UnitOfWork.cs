using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Infrastructure.Services.Books;
using LibrarySystem.Infrastructure.Services.Customers;
using LibrarySystem.Infrastructure.Services.Genres;
using LibrarySystem.Infrastructure.Services.Users;

namespace LibrarySystem.Infrastructure.Services;
internal class UnitOfWork(ISqlDataAccess _sqlDataAccess, IDateTimeProvider _dateTimeProvider) : IUnitOfWork
{
    // Data Access Services
    public ICustomerService Customers => new CustomerService(_sqlDataAccess, _dateTimeProvider);
    public IBookService Books => new BookService(_sqlDataAccess);
    public IUserService Users => new UserService(_sqlDataAccess, _dateTimeProvider);
    public IGenreService Genres => new GenreService(_sqlDataAccess);
}