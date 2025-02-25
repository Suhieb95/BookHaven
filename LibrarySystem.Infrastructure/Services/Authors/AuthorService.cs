using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;

namespace LibrarySystem.Infrastructure.Services.Authors;

public class AuthorService(ISqlDataAccess _sqlDataAccess) : IAuthorService
{

}
