using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Infrastructure.Services.Books;
using LibrarySystem.Infrastructure.Services.Customers;

namespace LibrarySystem.Infrastructure.Services;
public class UnitOfWork : IUnitOfWork
{
    private readonly ISqlDataAccess _sqlDataAccess;
    private readonly IDateTimeProvider _dateTimeProvider;
    public UnitOfWork(ISqlDataAccess sqlDataAccess, IDateTimeProvider dateTimeProvider)
    {
        _sqlDataAccess = sqlDataAccess;
        _dateTimeProvider = dateTimeProvider;

        CustomerService = new CustomerService(_sqlDataAccess, _dateTimeProvider);
        BookService = new BookService(_sqlDataAccess);
    }
    public ICustomerService CustomerService { get; }
    public IBookService BookService { get; }
}