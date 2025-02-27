using BookHaven.Application.Interfaces.Services;

namespace BookHaven.Application.Interfaces.Repositories;
public interface IUnitOfWork
{
    ICustomerService Customers { get; }
    IBookService Books { get; }
    IUserService Users { get; }
    IGenreService Genres { get; }
    IAuthorService Authors { get; }
    IBookImagesService BookImages { get; }
}