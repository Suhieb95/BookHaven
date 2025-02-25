using LibrarySystem.Application.Interfaces.Services;

namespace LibrarySystem.Application.Interfaces.Repositories;
public interface IUnitOfWork
{
    ICustomerService Customers { get; }
    IBookService Books { get; }
    IUserService Users { get; }
    IGenreService Genres { get; }
    IAuthorService Authors { get; }
}