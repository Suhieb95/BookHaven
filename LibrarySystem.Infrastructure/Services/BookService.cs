using LibrarySystem.Application.Books;
using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using MongoDB.Driver;
namespace LibrarySystem.Infrastructure.Services;
public class BookService(ISqlDataAccess _sqlDataAccess) : IBookService
{

}
