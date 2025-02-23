using LibrarySystem.Application.Interfaces.Services;

namespace LibrarySystem.Application.Interfaces.Repositories;
public interface IUnitOfWork
{
    public ICustomerService CustomerService { get; }
    public IBookService BookService { get; }
}