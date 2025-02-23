using LibrarySystem.Application.Interfaces.Services;

namespace LibrarySystem.Application.Interfaces.Repositories;
public interface IUnitOfWork
{
    public ICustomerService Customers { get; }
    public IBookService Books { get; }
}