using System.Net;
namespace LibrarySystem.Domain.Exceptions.BooksExceptions;
public class InvalidBookRatingException: Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public string ErrorMessage => "Invalid Book Rating.";
    public string Title => "Error";
}
