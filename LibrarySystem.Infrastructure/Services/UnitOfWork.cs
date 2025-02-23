using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Infrastructure.Services.Books;
using LibrarySystem.Infrastructure.Services.Customers;

namespace LibrarySystem.Infrastructure.Services;
internal class UnitOfWork(ISqlDataAccess sqlDataAccess, IDateTimeProvider dateTimeProvider) : IUnitOfWork
{
    private readonly ISqlDataAccess _sqlDataAccess = sqlDataAccess;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    // Data Access Services
    public ICustomerService Customers => new CustomerService(_sqlDataAccess, _dateTimeProvider);
    public IBookService Books => new BookService(_sqlDataAccess);
}